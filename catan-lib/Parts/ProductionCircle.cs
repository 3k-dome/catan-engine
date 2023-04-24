using CatanLib.Interfaces;

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

        public IEnumerable<float> ToVector()
        {
            return Encoding;
        }

        public IEnumerable<string> ToExplainedVector()
        {
            return new string[] { "NormalizedDiceRoll", "OddsOfDiceRoll" };
        }
    }
}
