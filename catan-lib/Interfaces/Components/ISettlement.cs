using CatanLib.Interfaces.Interaction;
using HexagonLib;

namespace CatanLib.Interfaces.Components
{
    public interface ISettlement : IVectorizableComponent, IActionUpgrade
    {
        VertexCoordinate Vertex { get; set; }
        bool IsSettlement { get; }
        bool IsCity { get; }
        IPlayer? Belongs { get; }
    }
}
