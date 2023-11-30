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
            [NotNull, ValidatedNotNull] Guid resultId,
            [NotNull, ValidatedNotNull] Guid correlationId,
            [NotNull, ValidatedNotNull] TValue value)
        {
            ResultId = Guard.Against.Null(resultId);
            CorrelationId = Guard.Against.Null(correlationId);
            Value = Guard.Against.Null(value);
        }

        public static Result<TValue> Create(Guid resultId, Guid correlationId, TValue value)
        {
            return new Result<TValue>(resultId, correlationId, value);
        }

        public static Result<TValue> Create(Guid resultId, Guid correlationId, IValueProvider<TValue> valueProvider)
        {
            return new Result<TValue>(resultId, correlationId, valueProvider.Value);
        }

        public static async Task<Result<TValue>> Create<TParams>(Guid resultId, Guid correlationId, IValueFactory<TValue> valueFactory, TParams parameters, CancellationToken cancellationToken)
        {
            return new Result<TValue>(resultId, correlationId, await valueFactory.CreateValueAsync(cancellationToken));
        }

        public Guid ResultId { get; }
        public Guid CorrelationId { get; }
        public TValue Value { get; }
    }
}
