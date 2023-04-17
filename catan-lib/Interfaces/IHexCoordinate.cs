using CatanLib.Enums;
using System.Data;

namespace CatanLib.Interfaces
{
    public interface IHexCoordinate
    {
        int Q { get; }
        int R { get; }
        int S { get; }

        (int Q, int R, int S) Key => (Q, R, R);

        void Constraint()
        {
            if (Q + R + S != 0)
            {
                throw new ArgumentException("Valid coordinates must obey 'Q + R + S == 0'.");
            }
        }

        public IHexCoordinate Add(IHexCoordinate coordinate);
        public IHexCoordinate Scale(int offset);
        public IHexCoordinate Neighbor(Direction direction);
    }
}
