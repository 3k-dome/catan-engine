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

                encoding = new float[] { Roll / 12f, Odds / 36f };
                return encoding;
            }
        }

        public IEnumerable<float> ToVector(ICatan catan)
        {
            return Encoding;
        }
    }
}
