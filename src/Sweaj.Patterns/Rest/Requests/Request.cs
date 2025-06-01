using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Rest.Requests
{

    /// <summary>
    /// Provides a base implementation of <see cref="IRequest{TValue}"/>, including a payload and cancellation token.
    /// </summary>
    /// <typeparam name="TValue">The type of the request payload.</typeparam>
    [Trackable]
    public abstract class Request<TValue> : IRequest<TValue?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request{TValue}"/> class.
        /// </summary>
        /// <param name="value">The payload of the request.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <c>null</c>.</exception>
        protected Request([NotNull, ValidatedNotNull] TValue? value, in CancellationToken cancellationToken)
        {
            RequestId = Guid.NewGuid();
            Value = Guard.Against.Null(value);
            CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Gets the unique identifier for this request.
        /// </summary>
        public Guid RequestId { get; }

        /// <inheritdoc/>
        public TValue? Value { get; }

        /// <inheritdoc/>
        public CancellationToken CancellationToken { get; }
    }
}
