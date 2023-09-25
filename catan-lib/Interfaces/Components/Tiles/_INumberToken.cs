namespace CatanLib.Interfaces.Components.Tiles;

public interface INumberToken
{
    char Symbol { get; }
    int Roll { get; }
    float Odds { get; }
}
