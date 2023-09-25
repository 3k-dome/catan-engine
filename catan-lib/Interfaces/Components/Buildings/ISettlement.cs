using CatanLib.Enums;
using HexagonLib;

namespace CatanLib.Interfaces.Components.Buildings;

public interface ISettlement : IBuilding
{
    VertexCoordinate Coordinate { get; }

    bool IsCity { get; }
    Piece CityPiece { get; }
    IEnumerable<Resource> CityCosts { get; }

    public void PlaceCity();
}
