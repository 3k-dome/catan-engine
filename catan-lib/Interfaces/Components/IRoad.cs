using CatanLib.Enums;
using CatanLib.Interfaces.Components;
using HexagonLib;

namespace CatanLib.Interfaces
{
    public interface IRoad : IVectorizableComponent, IActionPlay
    {
        EdgeCoordinate Edge { get; set; }
        bool IsRoad { get; }
        IPlayer? Belongs { get; }
    }
}
