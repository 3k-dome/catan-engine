namespace CatanLib.Exceptions;

/// <summary>
/// Thrown when a player won the game with the last action in the current step.
/// 
/// This is mostly used to propagate the event of a player winning the game
/// upstream though the call stack back to the upper game loop.
/// </summary>
public class PlayerHasWonException : Exception
{
}
