using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Requests
{
    public abstract class Request : ICorrelationIdProvider<Guid>
    {
        public Request(CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;
            CorrelationId = Guid.NewGuid();
        }

        public CancellationToken CancellationToken { get; }

        public Guid CorrelationId { get; }
    }

    public abstract class Request<TValue> : Request, IValueProvider<TValue>
    {
        protected Request([NotNull, ValidatedNotNull] TValue value, CancellationToken cancellationToken)
            : base(cancellationToken)
        {
            Value = Guard.Against.Null(value);
        }

        /// <summary>
        /// The payload of the request.
        /// </summary>
        public TValue Value { get; }
    }
}