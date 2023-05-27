using HexagonLib.Enums;

namespace HexagonLib.Operations;

public static class TileOperations
{
    public static IEnumerable<TileCoordinate> Circle(TileCoordinate center, TileNeighborDirection direction, int radius)
    {
        TileCoordinate offset = TileNeighbor.GetOffset(direction).Scale(radius);
        TileCoordinate corner = center.Add(offset);
        List<TileCoordinate> circle = new();

        TileNeighborDirection[] circularizationOrder = TileNeighbor.Directions.ToArray();
        circularizationOrder = ArrayOperations.Roll(circularizationOrder, (int)direction);
        circularizationOrder = ArrayOperations.Roll(circularizationOrder, 2);

        foreach (TileNeighborDirection next in circularizationOrder)
        {
            for (int i = 0; i < radius; i++)
            {
                circle.Add(corner);
                corner = corner.Add(TileNeighbor.GetOffset(next));
            }
        }

        return circle;
    }

    public static IEnumerable<TileCoordinate> Spiral(TileCoordinate center, TileNeighborDirection direction, int radius)
    {
        IEnumerable<TileCoordinate> tileCoordinates = Enumerable.Empty<TileCoordinate>();

        for (int i = radius; i > 0; i--)
        {
            tileCoordinates = tileCoordinates.Concat(Circle(center, direction, i));
        }
        tileCoordinates = tileCoordinates.Concat(new[] { center });

        return tileCoordinates;
    }
}
