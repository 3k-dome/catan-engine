using CatanLib.Interfaces;
using CatanLib.Parts;

namespace CatanLib.Enums
{
    public enum Direction
    {
        West,
        NorthWest,
        NorthEast,
        East,
        SouthEast,
        SouthWest,
    }

    public static class DirectionCoordinates
    {
        public static readonly Dictionary<Direction, IHexCoordinate> Coordinates = new()
        {
            { Direction.West, new HexCoordinate(1, 0, -1) },
            { Direction.NorthWest, new HexCoordinate(1, -1, 0) },
            { Direction.NorthEast, new HexCoordinate(0, -1, 1) },
            { Direction.East, new HexCoordinate(-1, 0, 1) },
            { Direction.SouthEast, new HexCoordinate(-1, 1, 0) },
            { Direction.SouthWest, new HexCoordinate(0, 1, -1) },
        };
    }
}
