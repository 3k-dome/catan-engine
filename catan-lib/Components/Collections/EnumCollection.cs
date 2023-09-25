using CatanLib.Interfaces.Components.Other;
using CatanLib.Interfaces.Interaction.Collections;

namespace CatanLib.Components.Collections;

public class EnumCollection<T> : IEnumCollection<T> where T : Enum
{
    private Dictionary<T, uint> DefaultItems { get; set; }
    public Dictionary<T, uint> Items { get; private set; }

    private float? NormalizationConstant { get; set; }
    private Dictionary<T, float>? NormalizationLookup { get; set; }

    public EnumCollection(Dictionary<T, uint> defaultItems, float normalization)
    {
        NormalizationConstant = normalization;
        DefaultItems = new(defaultItems);
        Items = new(defaultItems);
    }

    public EnumCollection(Dictionary<T, uint> defaultItems, Dictionary<T, float> normalization)
    {
        NormalizationLookup = new(normalization);
        DefaultItems = new(defaultItems);
        Items = new(defaultItems);
    }

    public void Add(T item)
    {
        Items[item] += 1;
    }

    public void AddRange(IEnumerable<T> items)
    {
        foreach (T resource in items)
        {
            Add(resource);
        }
    }

    public bool Contains(T item)
    {
        return Items[item] > 0;
    }

    public bool ContainsRange(IEnumerable<T> items)
    {
        return items.GroupBy(item => item, item => item)
            .All(group => Items[group.Key] >= group.Count());
    }

    public void Remove(T item)
    {
        if (Items[item] == 0)
        {
            throw new ArgumentException("There are no more resources of this type within this collection.");
        }

        Items[item] -= 1;
    }

    public void RemoveRange(IEnumerable<T> items)
    {
        foreach (T resource in items)
        {
            Remove(resource);
        }
    }

    public void Reset()
    {
        Items = new Dictionary<T, uint>(DefaultItems);
    }

    public uint Count()
    {
        return Items.Select(entry => entry.Value)
            .Aggregate(0u, (value, next) => value + next);
    }

    public uint Bound()
    {
        if (NormalizationConstant is not null)
        {
            return (uint)NormalizationConstant * (uint)Enum.GetValues(typeof(T)).Length;
        }

        return NormalizationLookup is not null
            ? NormalizationLookup.Select(entry => entry.Value)
                .Aggregate(0u, (value, next) => value + (uint)next)
            : throw new InvalidOperationException();
    }

    public IEnumerable<T> Available()
    {
        return Items.Where(entry => entry.Value > 0).Select(entry => entry.Key);
    }

    public IEnumerable<T> Flatten()
    {
        return Items.SelectMany(entry => Enumerable.Repeat(entry.Key, (int)entry.Value));
    }

    public IEnumerable<(T key, float value)> Encode()
    {
        if (NormalizationConstant is not null)
        {
            return Items.Select(entry => (entry.Key, entry.Value / (float)NormalizationConstant));
        }

        if (NormalizationLookup is not null)
        {
            return Items.Select(entry => (entry.Key, entry.Value / NormalizationLookup[entry.Key]));
        }
        
        throw new InvalidOperationException();
    }
}
