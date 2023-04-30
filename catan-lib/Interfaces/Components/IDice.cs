namespace CatanLib.Interfaces
{
    public interface IDice
    {
        Random Random { get; init; }
        int Rolled { get; }
        int Roll();
        int RollTwice();
    }
}
