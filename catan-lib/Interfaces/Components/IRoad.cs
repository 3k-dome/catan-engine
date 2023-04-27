using CatanLib.Enums;
using HexagonLib;

namespace CatanLib.Interfaces
{
    public interface IRoad : IVectorizableComponent, IVectorizableActions, IActionPlay
    {
        EdgeCoordinate Edge { get; set; }
        bool IsRoad { get; }
        PlayerNumber? Belongs { get; }
        void ActionPlace();
        bool CanPlace();
    }
}
