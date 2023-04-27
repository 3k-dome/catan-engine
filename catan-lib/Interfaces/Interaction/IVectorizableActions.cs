namespace CatanLib.Interfaces;
public interface IVectorizableActions
{
    IEnumerable<Action> GetActions();
    IEnumerable<Func<bool>> CanExecuteActions();
}
