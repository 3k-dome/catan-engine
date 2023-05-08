using CatanLib.Interfaces.Interaction;

namespace CatanLib.Interfaces.Components
{
    public interface IDice : IVectorizableComponent
    {
        Random Random { get; init; }
        int Rolled { get; }
        int Roll();
        int RollTwice();
    }
}
