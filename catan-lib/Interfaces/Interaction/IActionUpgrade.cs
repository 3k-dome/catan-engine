using CatanLib.Enums;
using CatanLib.Interfaces.Components;
using CatanLib.Parts;

namespace CatanLib.Interfaces.Interaction;
public interface IActionUpgrade : IActionPlay
{
    IEnumerable<ResourceType> UpgradeResourceCosts { get; }
    PieceType RequiredUpgradePiece { get; }

    void Upgrade<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
    where TSettlement : ISettlement, new()
    where TRoad : IRoad, new()
    where TDice : IDice, new();

    bool CanUpgrade<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
    where TSettlement : ISettlement, new()
    where TRoad : IRoad, new()
    where TDice : IDice, new();
}
