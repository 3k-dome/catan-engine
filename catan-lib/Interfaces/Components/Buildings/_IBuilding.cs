using CatanLib.Enums;
using CatanLib.Interfaces.Components.Other;

namespace CatanLib.Interfaces.Components.Buildings;

public interface IBuilding
{
    IPlayer? Owner { get; }
    Piece Piece { get; }
    IEnumerable<Resource> Costs { get; }

    public void SetOwner(IPlayer player);
}
