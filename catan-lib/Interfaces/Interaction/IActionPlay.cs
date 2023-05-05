using CatanLib.Enums;
using CatanLib.Interfaces.Components;
using CatanLib.Parts;

namespace CatanLib.Interfaces.Interaction;
public interface IActionPlay : IVectorizableActions
{
    IEnumerable<ResourceType> Costs { get; }

    void Play<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
    where TSettlement : ISettlement, new()
    where TRoad : IRoad, new()
    where TDice : IDice, new();

    bool CanPlay<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
    where TSettlement : ISettlement, new()
    where TRoad : IRoad, new()
    where TDice : IDice, new();
}
