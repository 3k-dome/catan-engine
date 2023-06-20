using CatanLib.Interfaces.Interaction.Other;

namespace CatanLib.Interfaces.Components.Other;

public interface IDice : IResetableBySeed
{
    Random Random { get; }
    int Roll();
}
