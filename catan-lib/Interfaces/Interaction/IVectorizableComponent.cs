using CatanLib.Interfaces.Components;
using CatanLib.Parts;

namespace CatanLib.Interfaces.Interaction
{
    public interface IVectorizableComponent
    {
        public IEnumerable<float> ToVector<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new();

        public IEnumerable<string> ToExplainedVector()
        {
            throw new NotImplementedException();
        }
    }
}
