namespace HexagonLib.Enums;

public enum TileVertexDirection
{
    North,
    NorthEast,
    SouthEast,
    South,
    SouthWest,
    NorthWest
}

public static class TileVertex
{
    public static IEnumerable<TileVertexDirection> Directions => Enum.GetValues<TileVertexDirection>();

    public static readonly Dictionary<TileVertexDirection, VertexCoordinate> Offsets = new()
    {
        { TileVertexDirection.North,     new(1, 0, 1) },
        { TileVertexDirection.NorthEast, new(0, 0, 1) },
        { TileVertexDirection.SouthEast, new(0, 1, 1) },
        { TileVertexDirection.South,     new(0, 1, 0) },
        { TileVertexDirection.SouthWest, new(1, 1, 0) },
        { TileVertexDirection.NorthWest, new(1, 0, 0) },
    };

    public static VertexCoordinate GetOffset(TileVertexDirection direction)
    {
        return Offsets[direction];
    }
}
