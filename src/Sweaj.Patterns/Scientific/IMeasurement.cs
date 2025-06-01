using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Scientific
{
    /// <summary>
    /// Represents a measurable value with an associated unit.
    /// </summary>
    /// <typeparam name="TValue">The type of the value being measured.</typeparam>
    /// <typeparam name="TUnit">The type representing the unit of measurement.</typeparam>
    /// <remarks>
    /// This interface combines a value provider with unit tracking, allowing consumers to access
    /// both the value and its unit in a structured way.
    /// </remarks>
    [Trackable]
    public interface IMeasurement<TValue, TUnit> : IValueProvider<TValue>
    {
        /// <summary>
        /// Gets or sets the unit associated with the measurement.
        /// </summary>
        TUnit Unit { get; set; }
    }
}

