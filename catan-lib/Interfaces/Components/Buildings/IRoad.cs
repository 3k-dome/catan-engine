using HexagonLib;

namespace CatanLib.Interfaces.Components.Buildings;

public interface IRoad : IBuilding
{
    EdgeCoordinate Edge { get; }
}
