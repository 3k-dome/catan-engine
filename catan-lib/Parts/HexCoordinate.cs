using CatanLib.Enums;
using CatanLib.Interfaces;

namespace CatanLib.Parts
{
    public class HexCoordinate : IHexCoordinate
    {
        public int Q { get; private set; }

        public int R { get; private set; }

        public int S { get; private set; }

        public HexCoordinate(int q, int r, int s)
        {
            (Q, R, S) = (q, r, s);
            ((IHexCoordinate)this).Constraint();
        }

        public IHexCoordinate Add(IHexCoordinate coordinate)
        {
            return new HexCoordinate(Q + coordinate.Q, R + coordinate.R, S + coordinate.S);
        }

        public IHexCoordinate Scale(int offset)
        {
            return new HexCoordinate(Q * offset, R * offset, S * offset);
        }

        public IHexCoordinate Neighbor(Direction direction)
        {
            return Add(DirectionCoordinates.Coordinates[direction]);
        }
    }
}