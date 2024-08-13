using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Data.Values
{
    /// <summary>
    /// Specifies that a derrived type has the necessary information to create itself asynchronously.
    /// </summary>
    /// <typeparam name="TValue">The value to create.</typeparam>
    [Trackable]
    public interface IValueFactory<TValue>
    {
        /// <summary>
        /// Specifies that a derrived type has the necessary information to create itself asynchronously.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TValue> CreateValueAsync(CancellationToken cancellationToken = default);
    }
}
