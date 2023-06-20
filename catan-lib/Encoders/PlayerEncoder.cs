using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Other;

namespace CatanLib.Encoders;

public static class PlayerEncoder
{
    /// <summary>
    /// Players must always be serialized in the same order depending
    /// on whose turn it is since the model always expects the values
    /// related to each player at the same place in each episode.
    /// </summary>
    public static IPlayer[] ProvidePlayerOrder(ICatan catan)
    {
        return catan.Players.Except(new[] { catan.CurrentPlayer })
            .OrderByDescending(player => player.Number)
            .Append(catan.CurrentPlayer)
            .Reverse()
            .ToArray();
    }

    /// <summary>
    /// Applies player order and encodes the owner ship of the given building.
    /// </summary>
    public static IEnumerable<float> EncodeOwnership(ICatan catan, IBuilding building)
    {
        IPlayer[] orderedPlayers = ProvidePlayerOrder(catan);

        float[] encoding = new float[orderedPlayers.Length];

        for (int i = 0; i < orderedPlayers.Length; i++)
        {
            if (building.Owner == orderedPlayers[i])
            {
                encoding[i] = 1;
            }
        }

        return encoding;
    }
}
