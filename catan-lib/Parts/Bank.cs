using CatanLib.Enums;
using CatanLib.Interfaces.Components;

namespace CatanLib.Parts
{
    public class Bank : IBank
    {
        public Dictionary<ResourceType, int> Resources { get; } = new()
        {
            { ResourceType.Sheep, 19 },
            { ResourceType.Wheat, 19 },
            { ResourceType.Ore, 19 },
            { ResourceType.Brick, 19 },
            { ResourceType.Wood, 19 },
        };

        public IEnumerable<float> ToVector<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            return Enum.GetValues<ResourceType>().Select(resource => Resources[resource] / 19f);
        }
    }
}
