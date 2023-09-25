using CatanLib.Enums;
using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Other;

namespace CatanLib.Components.Buildings;

public class Building : IBuilding
{
    public IPlayer? Owner { get; protected set; }
    public Piece Piece { get; protected set; }
    public IEnumerable<Resource> Costs { get; protected set; }

    public void SetOwner(IPlayer player)
    {
        if (Owner is not null)
        {
            throw new InvalidOperationException();
        }

        Owner = player;
    }

    public Building(Piece piece, IEnumerable<Resource> costs)
    {
        (Piece, Costs) = (piece, costs);
    }
}
