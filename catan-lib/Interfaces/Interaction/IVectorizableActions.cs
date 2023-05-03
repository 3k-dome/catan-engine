using CatanLib.Interfaces.Components;
using CatanLib.Parts;

namespace CatanLib.Interfaces.Interaction;
public interface IVectorizableActions
{
    IEnumerable<Action<Catan<TSettlement, TRoad, TDice>>> GetActions<TSettlement, TRoad, TDice>()
    where TSettlement : ISettlement, new()
    where TRoad : IRoad, new()
    where TDice : IDice, new();

    IEnumerable<Func<Catan<TSettlement, TRoad, TDice>, bool>> CanExecuteActions<TSettlement, TRoad, TDice>()
    where TSettlement : ISettlement, new()
    where TRoad : IRoad, new()
    where TDice : IDice, new();
}
