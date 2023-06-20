using CatanLib.Enums;
using CatanLib.Interfaces.Interaction.Collections;
using CatanLib.Interfaces.Interaction.Other;
using CatanLib.Interfaces.Interaction.Vectorization;

namespace CatanLib.Interfaces.Components.Other;

public interface IBank : IVectorizableComponent, IResetable
{
    IEnumCollection<Resource> Resources { get; }
}
