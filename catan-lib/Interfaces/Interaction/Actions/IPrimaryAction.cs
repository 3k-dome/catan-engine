using CatanLib.Interfaces.Components.Other;

namespace CatanLib.Interfaces.Interaction.Actions;

public interface IPrimaryAction
{
    void ExecutePrimary(ICatan catan);
    bool CanExecutePrimary(ICatan catan);
}
