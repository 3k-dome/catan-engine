using CatanLib.Enums;
using CatanLib.Interfaces.Components.Buildings;
using HexagonLib;

namespace CatanLib.Components.Buildings;

public class Settlement : Building, ISettlement
{
    public VertexCoordinate Coordinate { get; private set; }

    public bool IsCity { get; private set; } = false;
    public Piece CityPiece { get; } = Piece.City;
    public IEnumerable<Resource> CityCosts { get; } = new Resource[]
    {
        Resource.Wheat,
        Resource.Wheat,
        Resource.Ore,
        Resource.Ore,
        Resource.Ore,
    };

    public void PlaceCity()
    {
        if (IsCity)
        {
            throw new InvalidOperationException();
        }

        IsCity = true;
    }

    public Settlement(VertexCoordinate vertex) : base(Piece.Settlement, new Resource[] { Resource.Wood, Resource.Brick, Resource.Wheat, Resource.Sheep })
    {
        Coordinate = vertex;
    }
}
