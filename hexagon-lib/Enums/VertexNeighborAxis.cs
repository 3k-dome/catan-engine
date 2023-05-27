namespace HexagonLib.Enums;

public enum VertexNeighborAxis
{
    XAxis,
    YAxis,
    ZAxis
}

public static class VertexNeighbor
{
    public static IEnumerable<VertexNeighborAxis> Axes => Enum.GetValues<VertexNeighborAxis>();

    public static readonly Dictionary<VertexOrientation, Dictionary<VertexNeighborAxis, VertexCoordinate>> Offsets = new()
    {
        {
            VertexOrientation.CaretUp, new()
            {
                { VertexNeighborAxis.XAxis, new(1, 0, 0) },
                { VertexNeighborAxis.YAxis, new(0, 1, 0) },
                { VertexNeighborAxis.ZAxis, new(0, 0, 1) }
            }
        },
        {
            VertexOrientation.CaretDown, new()
            {
                { VertexNeighborAxis.XAxis, new(-1, 0, 0) },
                { VertexNeighborAxis.YAxis, new(0, -1, 0) },
                { VertexNeighborAxis.ZAxis, new(0, 0, -1) }
            }
        },
    };

    public static VertexCoordinate GetOffset(VertexOrientation orientation, VertexNeighborAxis axis)
    {
        return Offsets[orientation][axis];
    }
}
