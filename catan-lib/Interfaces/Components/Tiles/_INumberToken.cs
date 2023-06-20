using CatanLib.Interfaces.Interaction.Vectorization;

namespace CatanLib.Interfaces.Components.Tiles;

public interface INumberToken : IVectorizableComponent
{
    char Symbol { get; }
    int Roll { get; }
    float Odds { get; }
}
