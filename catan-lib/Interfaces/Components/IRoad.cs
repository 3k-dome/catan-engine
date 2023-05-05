using CatanLib.Interfaces.Interaction;
using HexagonLib;

namespace CatanLib.Interfaces.Components
{
    public interface IRoad : IVectorizableComponent, IActionPlay
    {
        EdgeCoordinate Edge { get; set; }
        bool IsRoad { get; }
        IPlayer? Belongs { get; }
    }
}
