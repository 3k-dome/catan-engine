using CatanLib.Interfaces.Components.Other;
using CatanLib.Parts;

namespace CatanLib.Interfaces.Interaction.Vectorization;

public interface IVectorizableActions
{
    IEnumerable<Action<ICatan>> GetActions();
    IEnumerable<Func<ICatan, bool>> CanExecuteActions();
    IEnumerable<string> GetActionDescriptions();
}
