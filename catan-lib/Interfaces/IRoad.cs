using CatanLib.Enums;
using HexagonLib;

namespace CatanLib.Interfaces
{
    public interface IRoad : IVectorizable
    {
        EdgeCoordinate Edge { get; set; }
        bool IsRoad { get; }
        PlayerNumber? Belongs { get; }
    }
}
