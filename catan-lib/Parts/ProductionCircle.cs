using CatanLib.Interfaces;

namespace CatanLib.Parts
{
    public class ProductionCircle : IProductionCircle
    {
        public char Order { get; init; }
        public int Limit { get; init; }
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

                encoding = new float[] { (float)Limit / 18, (float)Odds / 36 };
                return encoding;
            }
        }

        public IEnumerable<float> ToVector()
        {
            return Encoding;
        }
    }
}
