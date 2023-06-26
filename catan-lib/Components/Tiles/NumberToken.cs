using CatanLib.Interfaces.Components.Other;
using CatanLib.Interfaces.Components.Tiles;

namespace CatanLib.Components.Tiles;

public class NumberToken : INumberToken
{
    public char Symbol { get; private set; }
    public int Roll { get; private set; }
    public float Odds { get; private set; }

    public NumberToken(char symbol, int roll, float odds)
    {
        (Symbol, Roll, Odds) = (symbol, roll, odds);
    }

    public IEnumerable<float> ToVector(ICatan catan)
    {
        yield return Odds;
    }

    public IEnumerable<string> ToDescriptiveVector(ICatan catan)
    {
        yield return "Odds of Roll";
    }

    public static IEnumerable<INumberToken> TokenSet { get; } = new List<INumberToken>()
    {
        new NumberToken('B', 2, 1/36f),
        new NumberToken('D', 3, 2/36f),
        new NumberToken('Q', 3, 2/36f),
        new NumberToken('J', 4, 3/36f),
        new NumberToken('N', 4, 3/36f),
        new NumberToken('A', 5, 4/36f),
        new NumberToken('O', 5, 4/36f),
        new NumberToken('C', 6, 5/36f),
        new NumberToken('P', 6, 5/36f),
        new NumberToken('E', 8, 5/36f),
        new NumberToken('K', 8, 5/36f),
        new NumberToken('G', 9, 4/36f),
        new NumberToken('M', 9, 4/36f),
        new NumberToken('F', 10, 3/36f),
        new NumberToken('L', 10, 3/36f),
        new NumberToken('I', 11, 2/36f),
        new NumberToken('R', 11, 2/36f),
        new NumberToken('H', 12, 1/36f),
    };
    public static INumberToken DesertToken { get; } = new NumberToken(' ', 0, 6 / 36f);
}
