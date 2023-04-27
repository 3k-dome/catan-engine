using CatanLib.Enums;

namespace CatanLib.Interfaces;
public interface IActionUpgrade
{
    IEnumerable<ResourceType> UpgradeCosts { get; }

    void Upgrade();
    bool CanUpgrade();
}
