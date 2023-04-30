using CatanLib.Interfaces;

namespace CatanLib.Parts
{
    public class Dice : IDice
    {
        private readonly Random? random;
        public Random Random
        {
            get => random ?? throw new NullReferenceException();
            init => random = value;
        }

        public int Rolled { get; private set; }

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
