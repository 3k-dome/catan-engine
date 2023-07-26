namespace CatanLib.Exceptions;

/// <summary>
/// Thrown when the index of the selected action does not fit the shape of the action vector.
/// 
/// E.g. the selected index is negative or the selected index goes out
/// of bounds and therefore invalid.
/// </summary>
public class InvalidActionIndexException : Exception
{
}
