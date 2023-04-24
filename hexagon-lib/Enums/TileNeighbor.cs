namespace HexagonLib.Enums
{
    public enum TileNeighbor
    {
        West,
        NorthWest,
        NorthEast,
        East,
        SouthEast,
        SouthWest,
    }

    public static class TileNeighborCoordinates
    {
        public static readonly Dictionary<TileNeighbor, TileCoordinate> Offsets = new()
        {
            { TileNeighbor.West,      new TileCoordinate(1, 0, -1) },
            { TileNeighbor.NorthWest, new TileCoordinate(1, -1, 0) },
            { TileNeighbor.NorthEast, new TileCoordinate(0, -1, 1) },
            { TileNeighbor.East,      new TileCoordinate(-1, 0, 1) },
            { TileNeighbor.SouthEast, new TileCoordinate(-1, 1, 0) },
            { TileNeighbor.SouthWest, new TileCoordinate(0, 1, -1) },
        };
    }
}
