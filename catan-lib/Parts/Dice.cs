using CatanLib.Interfaces;

namespace CatanLib.Parts
{
    public class Dice : IDice
    {
        public Random Random { get; init; }

        public int Rolled { get; private set; }

        public Dice(int seed)
        {
            Random = new(seed);
        }

        public int Roll()
        {
            Rolled = Random.Next(1, 7);
            return Rolled;
        }

        public int RollTwice()
        {
            Rolled = Random.Next(1, 7) + Random.Next(1, 7);
            return Rolled;
        }
    }
}
