using CatanLib.Enums;
using CatanLib.Interfaces.Interaction.Other;
using CatanLib.Interfaces.Interaction.Vectorization;

namespace CatanLib.Interfaces.Components.Other;

public interface ICatan : IVectorizableComponent, IVectorizableActions, IResetableBySeed
{
    Phase CurrentPhase { get; }
    IPlayer CurrentPlayer { get; }

    IEnumerable<IPlayer> Players { get; }
    IDice Dices { get; }
    IBoard Board { get; }
    IBank Bank { get; }
}
