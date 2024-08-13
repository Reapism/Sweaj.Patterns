using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Data.Values
{
    /// <summary>
    /// Represents a value that can be retrieved from any context.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    [Trackable]
    public interface IValueProvider<TValue>
    {
        /// <summary>
        /// The value provided as a <typeparamref name="TValue"/>.
        /// </summary>
        public TValue Value { get; }
    }
}
