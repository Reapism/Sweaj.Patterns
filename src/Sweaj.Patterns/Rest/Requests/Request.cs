using Sweaj.Patterns.Data.Values;
using Sweaj.Patterns.Dates;

namespace Sweaj.Patterns.Rest.Requests
{
    public abstract class Request : ICorrelationIdProvider<Guid>
    {
        public Request(IDateTimeProvider dateTimeProvider, CancellationToken cancellationToken)
        {
            DateTimeProvider = Guard.Against.Null(dateTimeProvider);
            RequestTime = dateTimeProvider.Now();
            CancellationToken = cancellationToken;
            CorrelationId = Guid.NewGuid();
        }

        /// <summary>
        /// The time of the request using the <see cref="DateTimeProvider"/> instance.
        /// </summary>
        public DateTimeOffset RequestTime { get; }
        public IDateTimeProvider DateTimeProvider { get; }
        public CancellationToken CancellationToken { get; }

        public Guid CorrelationId { get; }
    }

    public abstract class Request<T> : Request, IValueProvider<T>
    {
        protected Request([NotNull, ValidatedNotNull] T value, IDateTimeProvider dateTimeProvider, CancellationToken cancellationToken)
            : base(dateTimeProvider, cancellationToken)
        {
            Value = Guard.Against.Null(value);
        }

        /// <summary>
        /// The payload of the request.
        /// </summary>
        public T Value { get; set; }
    }
}