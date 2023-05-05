using CatanLib.Interfaces.Components;
using CatanLib.Parts;

namespace CatanLib.Sets
{
    public static class ProductionCircleSet
    {
        public static readonly IEnumerable<IProductionCircle> Circles = new List<ProductionCircle>()
        {
            new() { Order = 'B', Roll = 2, Odds = 1 },

            new() { Order = 'D', Roll = 3, Odds = 2 },
            new() { Order = 'Q', Roll = 3, Odds = 2 },

            new() { Order = 'J', Roll = 4, Odds = 3 },
            new() { Order = 'N', Roll = 4, Odds = 3 },

            new() { Order = 'A', Roll = 5, Odds = 4 },
            new() { Order = 'O', Roll = 5, Odds = 4 },

            new() { Order = 'C', Roll = 6, Odds = 5 },
            new() { Order = 'P', Roll = 6, Odds = 5 },

            new() { Order = 'E', Roll = 8, Odds = 5 },
            new() { Order = 'K', Roll = 8, Odds = 5 },

            new() { Order = 'G', Roll = 9, Odds = 4 },
            new() { Order = 'M', Roll = 9, Odds = 4 },

            new() { Order = 'F', Roll = 10, Odds = 3 },
            new() { Order = 'L', Roll = 10, Odds = 3 },

            new() { Order = 'I', Roll = 11, Odds = 2 },
            new() { Order = 'R', Roll = 11, Odds = 2 },

            new() { Order = 'H', Roll = 12, Odds = 1 },
        };

        public static readonly IProductionCircle DesertCircle = new ProductionCircle() { Roll = 0, Odds = 0 };
    }
}

