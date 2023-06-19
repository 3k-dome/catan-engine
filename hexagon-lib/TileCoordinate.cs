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

    public TileVertexDirection GetAlignment()
    {
        IEnumerable<(char axis, int value)> coordinates = Enumerable.Empty<(char, int)>()
            .Append(('x', X))
            .Append(('y', Y))
            .Append(('z', Z))
            .ToArray();

        // corner tiles, the corner of an edge at ther outermost side of any edge 
        // of the hexagon belongs to the next edge see the frame pieces from catan
        if (coordinates.Where(tuple => tuple.value == 0).Any())
        {
            if (Math.Abs(X) == Math.Abs(Y))
            {
                return X > Y ? TileVertexDirection.SouthEast : TileVertexDirection.NorthWest;
            }

            if (Math.Abs(X) == Math.Abs(Z))
            {
                return X > Z ? TileVertexDirection.NorthEast : TileVertexDirection.SouthWest;
            }

            if (Math.Abs(Y) == Math.Abs(Z))
            {
                return Y > Z ? TileVertexDirection.North : TileVertexDirection.South;
            }
        }

        // any other tile between corners is a simple look up
        (char axis, int value) = coordinates.MaxBy(tuple => Math.Abs(tuple.value));
        return axis switch
        {
            'x' => value > 0 ? TileVertexDirection.NorthEast : TileVertexDirection.SouthWest,
            'y' => value > 0 ? TileVertexDirection.NorthWest : TileVertexDirection.SouthEast,
            'z' => value > 0 ? TileVertexDirection.South : TileVertexDirection.North,
            _ => throw new InvalidOperationException("Somehow this tile could not be matched.")
        };
    }
}
