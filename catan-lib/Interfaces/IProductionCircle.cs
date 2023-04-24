namespace CatanLib.Interfaces
{
    public interface IProductionCircle : IVectorizable
    {
        char Order { get; init; }
        int Roll { get; init; }
        float Odds { get; init; }
    }
}
