using CatanLib.Enums;
using CatanLib.Interfaces.Components.Tiles;
using HexagonLib;
using HexagonLib.Enums;

namespace CatanLib.Components.Tiles;

public class CostalTile : ICostalTile
{
    private readonly IEnumerable<TileVertexDirection>? defaultAccessibleFrom;
    public IEnumerable<TileVertexDirection>? AccessibleFrom { get; private set; }
    public Resource? TradesResource { get; private set; }
    public bool TradesAny { get; private set; }

    private TileCoordinate? coordinate;
    public TileCoordinate Coordinate
    {
        get => coordinate ?? throw new NullReferenceException("Coordinate not set.");
        set { coordinate = value; AlignAccess(); }
    }

    public CostalTile(IEnumerable<TileVertexDirection>? accessibleFrom, Resource? resource, bool any)
    {
        if (accessibleFrom is not null)
        {
            if (accessibleFrom.Count() != 2)
            {
                throw new ArgumentException("Harbors may only be accessed from two directions!");
            }

            if (accessibleFrom.Any() && resource is null && !any)
            {
                throw new ArgumentException("A harbor must provide trades if it is accessible!");
            }
        }

        if (resource is not null && any)
        {
            throw new ArgumentException("Harbors may only trade any resource or one specific at the time!");
        }

        (TradesResource, TradesAny, defaultAccessibleFrom) = (resource, any, accessibleFrom);
    }

    private void AlignAccess()
    {
        if (defaultAccessibleFrom is not null)
        {
            TileVertexDirection alignment = Coordinate.GetAlignment();

            AccessibleFrom = defaultAccessibleFrom.Select(direction => (int)direction + (int)alignment)
                .Select(direction => direction % TileVertex.Directions.Count())
                .Cast<TileVertexDirection>()
                .ToArray();
        }
        else
        {
            AccessibleFrom = null;
        }
    }
}
