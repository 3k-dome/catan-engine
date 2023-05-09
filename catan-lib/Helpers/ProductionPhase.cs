using CatanLib.Enums;
using CatanLib.Interfaces.Components;
using HexagonLib;
using System.Diagnostics;

namespace CatanLib.Helpers
{
    public static class ProductionPhase
    {
        public static IEnumerable<ITerrainTile> GetProducingTiles(Dictionary<TileCoordinate, IHexTile> tileStore, int roll)
        {
            return tileStore.Select(entry => entry.Value)
                .OfType<ITerrainTile>()
                .Where(tile => tile.Production.Roll == roll);
        }

        public static IEnumerable<ISettlement> GetPlacedSettlements(Dictionary<VertexCoordinate, ISettlement> vertexStore, ITerrainTile tile)
        {
            return tile.Coordinate.Vertices()
                .Select(vertexCoordinate => vertexStore[vertexCoordinate])
                .Where(settlement => settlement.Belongs is not null);
        }

        public static IEnumerable<ResourceType> GetTotalYield(Dictionary<VertexCoordinate, ISettlement> vertexStore, IEnumerable<ITerrainTile> tiles)
        {
            IEnumerable<ResourceType> totalYield = Enumerable.Empty<ResourceType>();

            foreach (ITerrainTile tile in tiles)
            {
                ResourceType resource = TerrainResources.Resources[tile.Terrain];
                IEnumerable<ISettlement> settlements = GetPlacedSettlements(vertexStore, tile);
                IEnumerable<ResourceType> tileProduction = settlements
                    .SelectMany(settlement => settlement.IsSettlement ? new[] { resource } : new[] { resource, resource });

                totalYield = totalYield.Concat(tileProduction);
            }

            return totalYield;
        }

        public static void DistributeResources(Dictionary<VertexCoordinate, ISettlement> vertexStore, IEnumerable<ITerrainTile> tiles, IBank bank)
        {
            foreach (ITerrainTile tile in tiles)
            {
                ResourceType resource = TerrainResources.Resources[tile.Terrain];
                IEnumerable<ISettlement> settlements = GetPlacedSettlements(vertexStore, tile);

                foreach (ISettlement settlement in settlements)
                {
                    if (settlement.IsSettlement)
                    {
                        GainResource(bank, settlement, resource);
                        continue;
                    }

                    if (settlement.IsCity)
                    {
                        GainResources(bank, settlement, new[] { resource, resource });
                        continue;
                    }
                }
            }
        }

        public static void GainResource(IBank bank, ISettlement settlement, ResourceType resource)
        {
            bank.UseResource(resource);
            settlement.Belongs?.GainResource(resource);
        }

        public static void GainResources(IBank bank, ISettlement settlement, IEnumerable<ResourceType> resources)
        {
            bank.UseResources(resources);
            settlement.Belongs?.GainResources(resources);
        }

        public static int GetPlayerCount(Dictionary<VertexCoordinate, ISettlement> vertexStore, IEnumerable<ITerrainTile> tiles)
        {
            return tiles.SelectMany(tile => GetPlacedSettlements(vertexStore, tile))
                .Select(settlement =>
                {
                    Debug.Assert(settlement.Belongs is not null);
                    return settlement.Belongs.Number;
                })
                .Distinct()
                .Count();
        }

        public static void TryDistributeResources(Dictionary<VertexCoordinate, ISettlement> vertexStore, IEnumerable<ITerrainTile> tiles, IBank bank)
        {
            foreach (ITerrainTile tile in tiles)
            {
                ResourceType resource = TerrainResources.Resources[tile.Terrain];
                IEnumerable<ISettlement> settlements = GetPlacedSettlements(vertexStore, tile);

                foreach (ISettlement settlement in settlements)
                {
                    if (settlement.IsSettlement)
                    {
                        TryGainResource(bank, settlement, resource);
                        continue;
                    }

                    if (settlement.IsCity)
                    {
                        TryGainResources(bank, settlement, new[] { resource, resource });
                        continue;
                    }
                }
            }
        }

        public static void TryGainResource(IBank bank, ISettlement settlement, ResourceType resource)
        {
            if (bank.HasResource(resource))
            {
                bank.UseResource(resource);
                settlement.Belongs?.GainResource(resource);
            }
        }

        public static void TryGainResources(IBank bank, ISettlement settlement, IEnumerable<ResourceType> resources)
        {
            foreach (ResourceType resource in resources)
            {
                TryGainResource(bank, settlement, resource);
            }
        }
    }
}
