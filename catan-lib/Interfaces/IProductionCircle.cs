namespace CatanLib.Interfaces
{
    public interface IProductionCircle : IVectorizable
    {
        char Order { get; init; }
        int Limit { get; init; }
        float Odds { get; init; }
    }
}
