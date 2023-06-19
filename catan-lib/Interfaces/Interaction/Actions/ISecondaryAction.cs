using CatanLib.Interfaces.Components.Other;

namespace CatanLib.Interfaces.Interaction.Actions;

public interface ISecondaryAction
{
    void ExecuteSecondary(ICatan catan);
    bool CanExecuteSecondary(ICatan catan);
}
