using HexagonLib.Enums;

namespace HexagonLib.Operations;

public static class TileOperations
{
    public static IEnumerable<TileCoordinate> Circle(TileCoordinate center, TileNeighborDirection direction, int radius)
    {
        TileCoordinate offset = TileNeighbor.GetOffset(direction).Scale(radius);
        TileCoordinate corner = center.Add(offset);
        List<TileCoordinate> circle = new();

        TileNeighborDirection[] offsetDirections = TileNeighbor.Directions.ToArray();
        offsetDirections = ArrayOperations.Roll(offsetDirections, (int)direction);
        offsetDirections = ArrayOperations.Roll(offsetDirections, 2);

        foreach (TileNeighborDirection offsetDirection in offsetDirections)
        {
            for (int i = 0; i < radius; i++)
            {
                circle.Add(corner);
                corner = corner.Add(TileNeighbor.GetOffset(offsetDirection));
            }
        }

        return circle;
    }

    public static IEnumerable<TileCoordinate> Spiral(TileCoordinate center, TileNeighborDirection direction, int radius)
    {
        IEnumerable<TileCoordinate> tileCoordinates = Enumerable.Empty<TileCoordinate>();

        for (int i = radius; i > 0; i--)
        {
            // circles are build clockwise but catans placement goes counter clockwise
            IEnumerable<TileCoordinate> circle = Circle(center, direction, i);
            circle = circle.Take(1).Concat(circle.Skip(1).Reverse());

            tileCoordinates = tileCoordinates.Concat(circle);
        }
        tileCoordinates = tileCoordinates.Append(center);

        return tileCoordinates;
    }
}
