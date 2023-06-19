namespace HexagonLib.Enums;

public enum TileNeighborDirection
{
    NorthEast,
    East,
    SouthEast,
    SouthWest,
    West,
    NorthWest,
}

public static class TileNeighbor
{
    public static IEnumerable<TileNeighborDirection> Directions => Enum.GetValues<TileNeighborDirection>();

    public static readonly Dictionary<TileNeighborDirection, TileCoordinate> Offsets = new()
    {
        { TileNeighborDirection.NorthEast, new(0, 1, -1) },
        { TileNeighborDirection.East,      new(-1, 1, 0) },
        { TileNeighborDirection.SouthEast, new(-1, 0, 1) },
        { TileNeighborDirection.SouthWest, new(0, -1, 1) },
        { TileNeighborDirection.West,      new(1, -1, 0) },
        { TileNeighborDirection.NorthWest, new(1, 0, -1) },
    };

    public static TileCoordinate GetOffset(TileNeighborDirection direction)
    {
        return Offsets[direction];
    }
}
