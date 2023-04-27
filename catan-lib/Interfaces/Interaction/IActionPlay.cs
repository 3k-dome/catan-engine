using CatanLib.Enums;
namespace CatanLib.Interfaces;
public interface IActionPlay
{
    IEnumerable<ResourceType> Costs { get; }

    void Play();
    bool CanPlay();
}
