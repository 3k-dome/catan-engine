using CatanLib.Enums;
using CatanLib.Interfaces.Components.Buildings;
using HexagonLib;

namespace CatanLib.Components.Buildings;

public class Road : Building, IRoad
{
    public EdgeCoordinate Edge { get; private set; }

    public Road(EdgeCoordinate edge) : base(Piece.Road, new Resource[] { Resource.Wood, Resource.Brick })
    {
        Edge = edge;
    }
}
