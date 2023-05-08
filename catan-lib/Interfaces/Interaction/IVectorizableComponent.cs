using CatanLib.Interfaces.Components;
using CatanLib.Parts;

namespace CatanLib.Interfaces.Interaction
{
    public interface IVectorizableComponent
    {
        public IEnumerable<float> ToVector(ICatan catan);

        public IEnumerable<string> ToExplainedVector(ICatan catan)
        {
            throw new NotImplementedException();
        }
    }
}
