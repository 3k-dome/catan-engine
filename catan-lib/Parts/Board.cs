using CatanLib.Enums;
using CatanLib.Interfaces;

namespace CatanLib.Parts
{
    public class Board
    {
        public Dictionary<(int, int, int), IHexTile> Tiles = new();

        public void SetupBoard()
        {
            IEnumerable<IHexCoordinate> hexCoordinates = Enumerable.Empty<IHexCoordinate>();

            IHexCoordinate center = new HexCoordinate(0, 0, 0);
            hexCoordinates = hexCoordinates.Concat(new[] { center });
            hexCoordinates = hexCoordinates.Concat(Circle(center, Direction.NorthWest, 1).Reverse());
            hexCoordinates = hexCoordinates.Concat(Circle(center, Direction.NorthWest, 2).Reverse());
            _ = hexCoordinates.Reverse();
        }

        public IEnumerable<IHexCoordinate> Circle(IHexCoordinate center, Direction direction, int radius)
        {
            // rings are created by walking starting from an edge coordinate
            // * therefore a direction is chosen and scaled by the given radius
            // * the direction is than added to our center coordinate to get the edge coordinate
            IHexCoordinate offset = DirectionCoordinates.Coordinates[direction].Scale(radius);
            IHexCoordinate edge = center.Add(offset);
            List<IHexCoordinate> circle = new();

            // the circularization direction is always offset by two from the edge direction
            // * therefore the array is rolled to the edge direction
            // * and than rolled again to be offset by two
            Direction[] circularizationOrder = Enum.GetValues<Direction>();
            circularizationOrder = Roll(circularizationOrder, (int)direction);
            circularizationOrder = Roll(circularizationOrder, 2);

            foreach (Direction next in circularizationOrder)
            {
                for (int i = 0; i < radius; i++)
                {
                    circle.Add(edge);
                    edge = edge.Add(DirectionCoordinates.Coordinates[next]);
                }
            }

            return circle;
        }

        public static T[] Roll<T>(T[] array, int positions)
        {
            if (positions >= array.Length)
            {
                throw new ArgumentException(
                    "This roll would rotate the entire array at least once."
                    + " Only non-repeating rotations are currently possible."
                );
            }

            T[] result = new T[array.Length];
            array[positions..].CopyTo(result, 0);
            array[..positions].CopyTo(result, array.Length - positions);
            return result;
        }

    }
}