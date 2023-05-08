using CatanLib.Enums;
using CatanLib.Interfaces.Components;

namespace CatanLib.Parts
{
    public class Player : IPlayer
    {
        public PlayerNumber Number { get; set; }

        public Dictionary<ResourceType, int> Resources { get; } = new()
        {
            { ResourceType.Sheep, 0 },
            { ResourceType.Wheat, 0 },
            { ResourceType.Ore, 0 },
            { ResourceType.Brick, 0 },
            { ResourceType.Wood, 0 }
        };

        public Dictionary<PieceType, int> Pieces { get; } = new()
        {
            { PieceType.Road, 15 },
            { PieceType.Settlement, 5 },
            { PieceType.City, 4 }
        };

        public IEnumerable<float> ToVector(ICatan catan)
        {
            float[] pieceEncoding = new float[]
            {
                Pieces[PieceType.Road] / 15f,
                Pieces[PieceType.Settlement] / 5f,
                Pieces[PieceType.City] / 4f
            };

            IEnumerable<float> resourceEncoding = Number == catan.CurrentPlayer.Number
                ? Enum.GetValues<ResourceType>().Select(resource => Resources[resource] / 19f)
                : new float[] { Enum.GetValues<ResourceType>().Select(resource => Resources[resource]).Sum() / 95f };

            return resourceEncoding.Concat(pieceEncoding);
        }
    }
}
