using CatanLib.Enums;
using CatanLib.Interfaces.Components;

namespace CatanLib.Parts
{
    public class Player : IPlayer
    {
        public PlayerNumber Number { get; set; }
        public int VictoryPoints { get; private set; }
        public int LongestRoadLength { get; set; }
        public bool HasLongestRoad { get; set; }

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

            return resourceEncoding.Concat(pieceEncoding).Append(VictoryPoints / 11f);
        }

        public void UpdateVictoryPoints(ICatan catan)
        {
            int points = 0;
            points += (5 - Pieces[PieceType.Settlement]) * 1;
            points += (4 - Pieces[PieceType.City]) * 2;
            points += HasLongestRoad ? 2 : 0;

            VictoryPoints = points;
        }
    }
}
