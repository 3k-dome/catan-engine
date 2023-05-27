using HexagonLib.Abstractions;
using HexagonLib.Enums;

namespace HexagonLib;

public class TileCoordinate : HexagonalCoordinate
{
    public TileCoordinate(int x, int y, int z) : base(x, y, z)
    {
        int sum = X + Y + Z;

        if (sum != 0)
        {
            throw new ArgumentException("Not a valid coordinate, tile coordinates must follow '0 == x + y + z'.");
        }
    }

    public override TileCoordinate Add((int X, int Y, int Z) coordinate)
    {
        return new(X + coordinate.X, Y + coordinate.Y, Z + coordinate.Z);
    }

    public override TileCoordinate Add(IHexagonalCoordinate coordinate)
    {
        return new(X + coordinate.X, Y + coordinate.Y, Z + coordinate.Z);
    }

    public TileCoordinate Scale(int scale)
    {
        return new(X * scale, Y * scale, Z * scale);
    }

    public TileCoordinate GetNeighbor(TileNeighborDirection direction)
    {
        TileCoordinate offset = TileNeighbor.GetOffset(direction);
        return Add(offset);
    }

    public IEnumerable<TileCoordinate> Neighbors()
    {
        foreach (TileNeighborDirection direction in TileNeighbor.Directions)
        {
            yield return GetNeighbor(direction);
        }
    }

    public VertexCoordinate GetVertex(TileVertexDirection direction)
    {
        VertexCoordinate offset = TileVertex.GetOffset(direction);
        return offset.Add(this);
    }

    public IEnumerable<VertexCoordinate> Vertices()
    {
        foreach (TileVertexDirection direction in TileVertex.Directions)
        {
            yield return GetVertex(direction);
        }
    }
}
