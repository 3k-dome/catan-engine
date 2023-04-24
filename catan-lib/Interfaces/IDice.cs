namespace CatanLib.Interfaces
{
    public interface IDice
    {
        Random Random { get; }
        int Rolled { get; }
        int Roll();
        int RollTwice();
    }
}
