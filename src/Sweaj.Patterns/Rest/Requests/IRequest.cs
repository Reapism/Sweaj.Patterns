using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Requests
{
    /// <summary>
    /// Represents a request with a payload and a cancellation token.
    /// </summary>
    /// <typeparam name="TValue">The type of the payload carried by the request.</typeparam>
    [Trackable]
    public interface IRequest<TValue> : IValueProvider<TValue>
    {
        /// <summary>
        /// Gets the cancellation token associated with the request.
        /// </summary>
        CancellationToken CancellationToken { get; }
    }
}
