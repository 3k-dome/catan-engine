namespace CatanLib.Exceptions;

/// <summary>
/// Thrown when the selected action is against the rules of the game.
/// 
/// E.g. the selected index fit the action vector but the action mask
/// states that the action at that index is not allowed in this state.
/// </summary>
public class InvalidActionSelectedException: Exception
{
}
