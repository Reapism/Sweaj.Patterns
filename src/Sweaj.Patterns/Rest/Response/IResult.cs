using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Response
{
    public interface IResult<TKey, TValue> : ICorrelationIdProvider<TKey>, IValueProvider<TValue>
        where TKey : IEquatable<TKey>
    { }

    public class Result<TValue> : IResult<Guid, TValue>
    {
        /// <summary>
        /// Initializes a result instance using the default constructor from
        /// </summary>
        protected Result(
            [NotNull, ValidatedNotNull] Guid correlationId,
            [NotNull, ValidatedNotNull] TValue value)
        {
            CorrelationId = Guard.Against.Null(correlationId);
            Value = Guard.Against.Null(value);

            ResultId = Guid.NewGuid();
        }

        public static Result<TValue> Create(Guid correlationId, TValue value)
        {
            return new Result<TValue>(correlationId, value);
        }

        public static Result<TValue> Create(Guid correlationId, IValueProvider<TValue> valueProvider)
        {
            return new Result<TValue>(correlationId, valueProvider.Value);
        }

        public static async Task<Result<TValue>> Create<TParams>(Guid correlationId, IValueFactory<TValue> valueFactory, TParams parameters, CancellationToken cancellationToken)
        {
            return new Result<TValue>(correlationId, await valueFactory.CreateValueAsync(cancellationToken));
        }

        public Guid ResultId { get; }
        public Guid CorrelationId { get; }
        public TValue Value { get; }
    }
}
