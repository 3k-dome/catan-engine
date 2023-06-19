using CatanLib.Enums;
using CatanLib.Interfaces.Components.Other;

namespace CatanLib.Interfaces.Interaction.Actions;

public interface ITrade
{
    IEnumerable<Resource> Offer { get; }
    void ExecuteTrade(ICatan catan);
    bool CanExecuteTrade(ICatan catan);
}
