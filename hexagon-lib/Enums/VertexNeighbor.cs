namespace HexagonLib.Enums
{
    public enum VertexNeighbor
    {
        XAxis,
        YAxis,
        ZAxis
    }

    public static class VertexNeighborCoordinates
    {
        public static readonly Dictionary<VertexType, Dictionary<VertexNeighbor, (int X, int Y, int Z)>> Offsets = new()
        {
            {
                VertexType.CaretUp, new()
                {
                    { VertexNeighbor.XAxis, (1, 0, 0) },
                    { VertexNeighbor.YAxis, (0, 1, 0) },
                    { VertexNeighbor.ZAxis, (0, 0, 1) }
                }
            },
            {
                VertexType.CaretDown, new()
                {
                    { VertexNeighbor.XAxis, (-1, 0, 0) },
                    { VertexNeighbor.YAxis, (0, -1, 0) },
                    { VertexNeighbor.ZAxis, (0, 0, -1) }
                }
            },
        };
    }
}
