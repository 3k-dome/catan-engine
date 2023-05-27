namespace HexagonLib.Enums;

public enum TileNeighborDirection
{
    West,
    NorthWest,
    NorthEast,
    East,
    SouthEast,
    SouthWest,
}

public static class TileNeighbor
{
    public static IEnumerable<TileNeighborDirection> Directions => Enum.GetValues<TileNeighborDirection>();

    public static readonly Dictionary<TileNeighborDirection, TileCoordinate> Offsets = new()
    {
        { TileNeighborDirection.West,      new(1, 0, -1) },
        { TileNeighborDirection.NorthWest, new(1, -1, 0) },
        { TileNeighborDirection.NorthEast, new(0, -1, 1) },
        { TileNeighborDirection.East,      new(-1, 0, 1) },
        { TileNeighborDirection.SouthEast, new(-1, 1, 0) },
        { TileNeighborDirection.SouthWest, new(0, 1, -1) },
    };

    public static TileCoordinate GetOffset(TileNeighborDirection direction)
    {
        return Offsets[direction];
    }
}
