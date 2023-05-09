using CatanLib.Enums;
using CatanLib.Interfaces.Interaction;

namespace CatanLib.Interfaces.Components;

public interface IPlayer : IResourceCollection, IPieceCollection, IVectorizableComponent
{
    PlayerNumber Number { get; set; }

    int VictoryPoints { get; }

    void UpdateVictoryPoints(ICatan catan, int offset);
}
