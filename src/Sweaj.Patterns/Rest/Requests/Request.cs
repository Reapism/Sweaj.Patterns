using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Requests
{
    /// <summary>
    /// Represents a request with some payload and a cancellation token.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    [Trackable]
    public interface IRequest<TValue> : IValueProvider<TValue>         
    {
        CancellationToken CancellationToken { get; }
    }

    /// <summary>
    /// Represents a request with a payload, and a cancellation token.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    [Trackable]
    public abstract class Request<TValue> : IRequest<TValue?>
    {
        protected Request([NotNull, ValidatedNotNull] TValue? value, in CancellationToken cancellationToken)
        {
            RequestId = Guid.NewGuid();
            Value = Guard.Against.Null(value);
            CancellationToken = cancellationToken;
        }
        /// <summary>
        /// The unique ID of this request.
        /// </summary>
        public Guid RequestId { get; }
        /// <summary>
        /// The payload of the request.
        /// </summary>
        public TValue? Value { get; }
        /// <summary>
        /// A cancellation token for this request should it be cancelled.
        /// </summary>
        public CancellationToken CancellationToken { get; }
    }

    /// <summary>
    /// Represents a generic Api Request with a payload, correlation ID, and a cancellation token
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    [Trackable]
    public abstract class ApiRequest<TValue> : Request<TValue>, ICorrelationIdProvider<Guid>
    {
        protected ApiRequest(in Guid correlationId, [NotNull, ValidatedNotNull] TValue? value, in CancellationToken cancellationToken)
            : base(value, cancellationToken)
        {
            CorrelationId = correlationId;
        }

        protected ApiRequest([NotNull, ValidatedNotNull] TValue? value, in CancellationToken cancellationToken)
            : base(value, cancellationToken)
        {
            CorrelationId = Guid.NewGuid();
        }

        public Guid CorrelationId { get; }
    }
}