using CatanLib.Encoders;
using CatanLib.Enums;
using CatanLib.Interfaces.Components.Other;
using CatanLib.Interfaces.Components.Tiles;
using HexagonLib;

namespace CatanLib.Components.Tiles;

public class TerrainTile : ITerrainTile
{
    public Terrain? TerrainType { get; private set; }
    public INumberToken NumberToken { get; private set; }
    public TileCoordinate Coordinate { get; private set; }

    public TerrainTile(Terrain? terrain, INumberToken token, TileCoordinate coordinate)
    {
        (TerrainType, NumberToken, Coordinate) = (terrain, token, coordinate);
        Encoding = new(() => TerrainEncoder.EncodeTerrain(terrain));
        DescriptiveEncoding = new(() => TerrainEncoder.EncodeTerrainDescriptive());
    }

    public Lazy<IEnumerable<float>> Encoding { get; private set; }
    public Lazy<IEnumerable<string>> DescriptiveEncoding { get; private set; }

    public IEnumerable<float> ToVector(ICatan catan)
    {
        return Encoding.Value.Concat(NumberToken.ToVector(catan));
    }

    public IEnumerable<string> ToDescriptiveVector(ICatan catan)
    {
        return DescriptiveEncoding.Value.Concat(NumberToken.ToDescriptiveVector(catan));
    }
}
