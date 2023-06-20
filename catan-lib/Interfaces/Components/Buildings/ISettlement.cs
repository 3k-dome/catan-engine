using CatanLib.Interfaces.Interaction.Actions;
using CatanLib.Interfaces.Interaction.Vectorization;
using HexagonLib;

namespace CatanLib.Interfaces.Components.Buildings;

public interface ISettlement : IBuilding, IUpgradeable, IPrimaryAction, ISecondaryAction, IVectorizableComponent
{
    VertexCoordinate Coordinate { get; }
}
