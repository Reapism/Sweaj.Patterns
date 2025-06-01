using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Rest.Requests
{
    /// <summary>
    /// Represents an API request that includes a correlation ID, payload, and cancellation token.
    /// </summary>
    /// <typeparam name="TValue">The type of the payload carried by the request.</typeparam>
    [Trackable]
    public abstract class ApiRequest<TValue> : Request<TValue>, ICorrelationIdProvider<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiRequest{TValue}"/> class with a specified correlation ID.
        /// </summary>
        /// <param name="correlationId">A correlation ID to trace the request through the system.</param>
        /// <param name="value">The payload of the request.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        protected ApiRequest(in Guid correlationId, [NotNull, ValidatedNotNull] TValue? value, in CancellationToken cancellationToken)
            : base(value, cancellationToken)
        {
            CorrelationId = correlationId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiRequest{TValue}"/> class with a generated correlation ID.
        /// </summary>
        /// <param name="value">The payload of the request.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        protected ApiRequest([NotNull, ValidatedNotNull] TValue? value, in CancellationToken cancellationToken)
            : base(value, cancellationToken)
        {
            CorrelationId = Guid.NewGuid();
        }

        /// <inheritdoc/>
        public Guid CorrelationId { get; }
    }
}
