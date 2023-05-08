using CatanLib.Enums;
using CatanLib.Interfaces.Components;


namespace CatanLib.Interfaces.Interaction;
public interface IActionPlay : IVectorizableActions
{
    IEnumerable<ResourceType> ResourceCosts { get; }
    PieceType RequiredPiece { get; }

    void Play(ICatan catan);

    bool CanPlay(ICatan catan);
}
