namespace CatanLib.Enums;

public enum Phase
{
    Settlement,
    RobberPlace,
    RobberDiscard,
    TradeOffer,
    TradeTake,
    Building,
}

public static class PhaseEncoder
{
    public static IEnumerable<Phase> Phases => Enum.GetValues<Phase>();

    public static IEnumerable<float> EncodePhase(Phase current)
    {
        return Phases.Select(phase => phase == current ? 1f : 0f);
    }
}
