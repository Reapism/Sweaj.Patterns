using Sweaj.Patterns.Attributes;
using System;

namespace Sweaj.Patterns.Rest
{
    /// <summary>
    /// Provides a correlation identifier used to track requests across different layers in an application.
    /// </summary>
    /// <typeparam name="TKey">The type of the correlation identifier. Must implement <see cref="IEquatable{TKey}"/>.</typeparam>
    [Trackable]
    public interface ICorrelationIdProvider<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets the correlation identifier associated with a request.
        /// </summary>
        TKey CorrelationId { get; }
    }
}
