using CatanLib.Encoders;
using CatanLib.Enums;
using CatanLib.Interfaces.Components.Other;
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
                throw new ArgumentException("Harbors may only be accessed from to directions!");
            }

            if (accessibleFrom.Any() && resource is null && !any)
            {
                throw new ArgumentException("A harbory must provide trades if it is accessibe!");
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
                .Select(direction => direction % (TileVertex.Directions.Count() - 1))
                .Cast<TileVertexDirection>()
                .ToArray();
        }
        else
        {
            AccessibleFrom = null;
        }
    }

    public IEnumerable<float> ToVector(ICatan catan)
    {
        IEnumerable<float> encoding = ResourceEncoder.EncodeResource(TradesResource);
        return encoding.Append(TradesAny ? 1f : 0f);
    }

    public IEnumerable<string> ToDescriptiveVector(ICatan catan)
    {
        IEnumerable<string> encoding = ResourceEncoder.EncodeResourceDescriptive();
        return encoding.Append("Trades any 3:1");
    }
}
