using CatanLib.Interfaces.Components;

namespace CatanLib.Interfaces.Interaction;
public interface IVectorizableActions
{
    IEnumerable<Action<ICatan>> GetActions();

    IEnumerable<Func<ICatan, bool>> CanExecuteActions();
}
