using CatanLib.Enums;

namespace CatanLib.Interfaces.Components.Buildings;

public interface IUpgradeable
{
    bool IsUpgraded { get; }
    Piece UpgradedPiece { get; }
    IEnumerable<Resource> UpgradeCosts { get; }
}
