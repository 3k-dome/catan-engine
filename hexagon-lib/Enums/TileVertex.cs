namespace HexagonLib.Enums
{
    public enum TileVertex
    {
        North,
        NorthEast,
        SouthEast,
        South,
        SouthWest,
        NorthWest
    }

    public static class TileVertexCoordinates
    {
        public static readonly Dictionary<TileVertex, (int X, int Y, int Z)> Offsets = new()
        {
            { TileVertex.North,     (1, 0, 1) },
            { TileVertex.NorthEast, (0, 0, 1) },
            { TileVertex.SouthEast, (0, 1, 1) },
            { TileVertex.South,     (0, 1, 0) },
            { TileVertex.SouthWest, (1, 1, 0) },
            { TileVertex.NorthWest, (1, 0, 0) },
        };
    }
}
