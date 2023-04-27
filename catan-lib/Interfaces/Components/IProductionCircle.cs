namespace CatanLib.Interfaces
{
    public interface IProductionCircle : IVectorizableComponent
    {
        char Order { get; init; }
        int Roll { get; init; }
        float Odds { get; init; }
    }
}
