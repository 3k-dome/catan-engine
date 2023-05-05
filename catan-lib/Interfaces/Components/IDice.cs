namespace CatanLib.Interfaces.Components
{
    public interface IDice
    {
        Random Random { get; init; }
        int Rolled { get; }
        int Roll();
        int RollTwice();
    }
}
