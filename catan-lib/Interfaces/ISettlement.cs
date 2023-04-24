using CatanLib.Enums;
using HexagonLib;

namespace CatanLib.Interfaces
{
    public interface ISettlement : IVectorizable
    {
        VertexCoordinate Vertex { get; set; }
        bool IsSettlement { get; }
        bool IsCity { get; }
        PlayerNumber? Belongs { get; }
    }
}
