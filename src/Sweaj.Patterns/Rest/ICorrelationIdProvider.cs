using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Rest
{
    /// <summary>
    /// Provides a correlation identifier used to track requests between different layers in an application.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    [Trackable]
    public interface ICorrelationIdProvider<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey CorrelationId { get; }
    }
}
