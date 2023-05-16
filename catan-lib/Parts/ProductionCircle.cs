using CatanLib.Interfaces.Components;

namespace CatanLib.Parts
{
    public class ProductionCircle : IProductionCircle
    {
        public char Order { get; init; }
        public int Roll { get; init; }
        public float Odds { get; init; }

        public IEnumerable<float> ToVector(ICatan catan)
        {
            yield return Odds / 36f;
        }
    }
}
