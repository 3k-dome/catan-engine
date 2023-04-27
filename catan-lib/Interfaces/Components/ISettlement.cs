using CatanLib.Enums;
using HexagonLib;

namespace CatanLib.Interfaces
{
    public interface ISettlement : IVectorizableComponent, IVectorizableActions, IActionPlay, IActionUpgrade
    {
        VertexCoordinate Vertex { get; set; }
        bool IsSettlement { get; }
        bool IsCity { get; }
        PlayerNumber? Belongs { get; }
    }
}
