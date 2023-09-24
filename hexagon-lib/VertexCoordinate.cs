using HexagonLib.Abstractions;
using HexagonLib.Enums;

namespace HexagonLib;

public class VertexCoordinate : HexagonalCoordinate
{
    public VertexOrientation Orientation { get; }

    public VertexCoordinate(int x, int y, int z) : base(x, y, z)
    {
        int sum = X + Y + Z;

        Orientation = sum switch
        {
            0 => throw new ArgumentException("Tile coordinate was given."),
            1 => VertexOrientation.CaretUp,
            2 => VertexOrientation.CaretDown,
            _ => throw new ArgumentException("Coordinate seems to be on a different plane."),
        };
    }

    public override VertexCoordinate Add((int X, int Y, int Z) coordinate)
    {
        return new(X + coordinate.X, Y + coordinate.Y, Z + coordinate.Z);
    }

    public override VertexCoordinate Add(IHexagonalCoordinate coordinate)
    {
        return new(X + coordinate.X, Y + coordinate.Y, Z + coordinate.Z);
    }

    public VertexCoordinate GetNeighbor(VertexNeighborAxis axis)
    {
        (int X, int Y, int Z) offset = VertexNeighbor.GetOffset(Orientation, axis);
        return Add(offset);
    }

    public IEnumerable<VertexCoordinate> Neighbors()
    {
        foreach (VertexNeighborAxis axis in VertexNeighbor.Axes)
        {
            yield return GetNeighbor(axis);
        }
    }

    public EdgeCoordinate GetEdge(VertexNeighborAxis axis)
    {
        VertexCoordinate secondary = GetNeighbor(axis);
        return new(this, secondary);
    }

    public IEnumerable<EdgeCoordinate> Edges()
    {
        foreach (VertexNeighborAxis axis in VertexNeighbor.Axes)
        {
            yield return GetEdge(axis);
        }
    }

    public IEnumerable<TileCoordinate> GetTiles()
    {
        return Orientation switch
        {
            VertexOrientation.CaretUp => new TileCoordinate[] { new(X - 1, Y, Z), new(X, Y - 1, Z), new(X, Y, Z - 1) },
            VertexOrientation.CaretDown => new TileCoordinate[] { new(X - 1, Y - 1, Z), new(X, Y - 1, Z - 1), new(X - 1, Y, Z - 1) },
            _ => throw new NotImplementedException()
        };
    }
}
