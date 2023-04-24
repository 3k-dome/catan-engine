﻿using HexagonLib.Enums;

namespace HexagonLib.Utils
{
    public static class TileOperations
    {
        public static IEnumerable<TileCoordinate> Circle(TileCoordinate center, TileNeighbor direction, int radius)
        {
            TileCoordinate offset = TileNeighborCoordinates.Offsets[direction].Scale(radius);
            TileCoordinate edge = center.Add(offset);
            List<TileCoordinate> circle = new();

            TileNeighbor[] circularizationOrder = Enum.GetValues<TileNeighbor>();
            circularizationOrder = ArrayOperations.Roll(circularizationOrder, (int)direction);
            circularizationOrder = ArrayOperations.Roll(circularizationOrder, 2);

            foreach (TileNeighbor next in circularizationOrder)
            {
                for (int i = 0; i < radius; i++)
                {
                    circle.Add(edge);
                    edge = edge.Add(TileNeighborCoordinates.Offsets[next]);
                }
            }

            return circle;
        }
    }
}