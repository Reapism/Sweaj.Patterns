using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Scientific
{
    [Trackable]
    public interface IMeasurement<TValue, TUnit> : IValueProvider<TValue>
    {
        TUnit Unit { get; set; }
    }
}

