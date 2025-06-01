using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Scientific
{
    /// <summary>
    /// Represents a range of values with defined minimum and maximum bounds.
    /// </summary>
    /// <typeparam name="TValue">The type of the values within the range.</typeparam>
    [Trackable]
    public interface IRange<TValue>
    {
        /// <summary>
        /// Gets the minimum value of the range.
        /// </summary>
        /// <returns>The lower bound of type <typeparamref name="TValue"/>.</returns>
        TValue Min();

        /// <summary>
        /// Gets the maximum value of the range.
        /// </summary>
        /// <returns>The upper bound of type <typeparamref name="TValue"/>.</returns>
        TValue Max();
    }

}
