using CatanLib.Interfaces.Interaction.Actions;
using CatanLib.Interfaces.Interaction.Vectorization;
using HexagonLib;

namespace CatanLib.Interfaces.Components.Buildings;

public interface IRoad : IBuilding, IPrimaryAction, IVectorizableComponent
{
    EdgeCoordinate Edge { get; }
}
