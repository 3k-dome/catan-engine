using CatanLib.Enums;
using CatanLib.Interfaces.Interaction.Collections;
using CatanLib.Interfaces.Interaction.Vectorization;

namespace CatanLib.Interfaces.Components.Other;

public interface IPlayer : IVectorizableComponent
{
    PlayerNumber Number { get; }
    uint LongestRoadLength { get; }
    bool HasLongestRoad { get; }
    uint VictoryPoints { get; }
    IEnumCollection<Piece> Pieces { get; }
    IEnumCollection<Resource> Resources { get; }

    void SetNumer(PlayerNumber number);
    void UpdateLongestRoad();
    void UpdateVictoryPoints();
}
