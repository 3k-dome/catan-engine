using CatanLib.Enums;
using CatanLib.Interfaces.Components.Tiles;
using HexagonLib.Enums;

namespace CatanLib.Components.Tiles;

public class CostalFrame : ICostalFrame
{
    public ICostalTile[] Tiles { get; private set; }

    private CostalFrame(ICostalTile[] tiles)
    {
        if (tiles.Length != 3)
        {
            throw new ArgumentException("A frame must consist of exactly 3 tiles!");
        }

        Tiles = tiles;
    }

    public static IEnumerable<ICostalFrame> Frames { get; } = new List<ICostalFrame>()
    {
        // 2, 1
        new CostalFrame(new CostalTile[]
        {
            new CostalTile(new[] { TileVertexDirection.South, TileVertexDirection.SouthEast }, null, true),
            new CostalTile(null, null, false),
            new CostalTile(new[] { TileVertexDirection.SouthWest, TileVertexDirection.South }, Resource.Sheep, false)

        }),

        // 3, 2
        new CostalFrame(new CostalTile[]
        {
            new CostalTile(null, null, false),
            new CostalTile(new[] { TileVertexDirection.South, TileVertexDirection.SouthEast }, Resource.Ore, false),
            new CostalTile(null, null, false),
        }),

        // 4, 3
        new CostalFrame(new CostalTile[]
        {
            new CostalTile(new[] { TileVertexDirection.South, TileVertexDirection.SouthEast }, null, true),
            new CostalTile(null, null, false),
            new CostalTile(new[] { TileVertexDirection.SouthWest, TileVertexDirection.South }, Resource.Wheat, false)
        }),

        // 5, 3
        new CostalFrame(new CostalTile[]
        {
            new CostalTile(null, null, false),
            new CostalTile(new[] { TileVertexDirection.South, TileVertexDirection.SouthEast }, Resource.Wood, false),
            new CostalTile(null, null, false),
        }),

        // 6, 5
        new CostalFrame(new CostalTile[]
        {
            new CostalTile(new[] { TileVertexDirection.South, TileVertexDirection.SouthEast }, null, true),
            new CostalTile(null, null, false),
            new CostalTile(new[] { TileVertexDirection.SouthWest, TileVertexDirection.South }, Resource.Brick, false)
        }),

        // 1, 6
        new CostalFrame(new CostalTile[]
        {
            new CostalTile(null, null, false),
            new CostalTile(new[] { TileVertexDirection.South, TileVertexDirection.SouthEast }, null, true),
            new CostalTile(null, null, false),
        }),
    };
}
