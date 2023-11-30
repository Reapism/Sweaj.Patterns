using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Requests
{
    public interface IRequest<TKey, TValue> : ICorrelationIdProvider<TKey>, IValueProvider<TValue>
         where TKey : IEquatable<TKey>, new()
    {
        CancellationToken CancellationToken { get; }
    }

    public abstract class Request<TValue> : IRequest<Guid, TValue>
    {
        protected Request(Guid correlationId, [NotNull, ValidatedNotNull] TValue value, CancellationToken cancellationToken)
        {
            Value = Guard.Against.Null(value);
            CorrelationId = correlationId;
            CancellationToken = cancellationToken;
        }

        /// <summary>
        /// The payload of the request.
        /// </summary>
        public TValue Value { get; }
        public Guid CorrelationId { get; }
        public CancellationToken CancellationToken { get; }
    }
}