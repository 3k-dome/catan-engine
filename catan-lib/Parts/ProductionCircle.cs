using CatanLib.Interfaces.Components;

namespace CatanLib.Parts
{
    public class ProductionCircle : IProductionCircle
    {
        public char Order { get; init; }
        public int Roll { get; init; }
        public float Odds { get; init; }

        private float[]? encoding;
        public float[] Encoding
        {
            get
            {
                if (encoding != null)
                {
                    return encoding;
                }

                encoding = new float[] { (float)Roll / 12, (float)Odds / 36 };
                return encoding;
            }
        }

        public IEnumerable<float> ToVector<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
            where TSettlement : ISettlement, new()
            where TRoad : IRoad, new()
            where TDice : IDice, new()
        {
            return Encoding;
        }
    }
}
