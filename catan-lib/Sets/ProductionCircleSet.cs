using CatanLib.Interfaces;
using CatanLib.Parts;

namespace CatanLib.Sets
{
    public static class ProductionCircleSet
    {
        public static readonly IEnumerable<IProductionCircle> Circles = new List<ProductionCircle>()
        {
            new() { Order = 'B', Limit = 2, Odds = 1 },

            new() { Order = 'D', Limit = 3, Odds = 2 },
            new() { Order = 'Q', Limit = 3, Odds = 2 },

            new() { Order = 'J', Limit = 4, Odds = 3 },
            new() { Order = 'N', Limit = 4, Odds = 3 },

            new() { Order = 'A', Limit = 5, Odds = 4 },
            new() { Order = 'O', Limit = 5, Odds = 4 },

            new() { Order = 'C', Limit = 6, Odds = 5 },
            new() { Order = 'P', Limit = 6, Odds = 5 },

            new() { Order = 'E', Limit = 8, Odds = 5 },
            new() { Order = 'K', Limit = 8, Odds = 5 },

            new() { Order = 'G', Limit = 9, Odds = 4 },
            new() { Order = 'M', Limit = 9, Odds = 4 },

            new() { Order = 'F', Limit = 10, Odds = 3 },
            new() { Order = 'L', Limit = 10, Odds = 3 },

            new() { Order = 'I', Limit = 11, Odds = 2 },
            new() { Order = 'R', Limit = 11, Odds = 2 },

            new() { Order = 'H', Limit = 12, Odds = 1 },
        };

        public static readonly IProductionCircle DesertCircle = new ProductionCircle() { Limit = 0, Odds = 0 };
    }
}

