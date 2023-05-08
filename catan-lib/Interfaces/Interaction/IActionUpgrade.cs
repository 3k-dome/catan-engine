using CatanLib.Enums;
using CatanLib.Interfaces.Components;

namespace CatanLib.Interfaces.Interaction;
public interface IActionUpgrade : IActionPlay
{
    IEnumerable<ResourceType> UpgradeResourceCosts { get; }
    PieceType RequiredUpgradePiece { get; }

    void Upgrade(ICatan catan);

    bool CanUpgrade(ICatan catan);
}
