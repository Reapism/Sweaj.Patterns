using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Response
{
    public interface IResult<TKey> : ICorrelationIdProvider<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey ResultId { get; }
    }

    public interface IResult<TKey, TValue> : IResult<TKey>, IValueProvider<TValue>
        where TKey : IEquatable<TKey>
    { }

    public class Result<TKey, TValue> : IResult<TKey, TValue>
        where TKey : IEquatable<TKey>, new()
    {
        /// <summary>
        /// Initializes a result instance using the default constructor from
        /// </summary>
        protected Result(
            [NotNull, ValidatedNotNull] TKey resultId,
            [NotNull, ValidatedNotNull] TKey correlationId,
            [NotNull, ValidatedNotNull] TValue value)
        {
            ResultId = Guard.Against.Null(resultId);
            CorrelationId = Guard.Against.Null(correlationId);
            Value = Guard.Against.Null(value);
        }

        public static Result<TKey, TValue> Create(TKey resultId, TKey correlationId, TValue value)
        {
            return new Result<TKey, TValue>(resultId, correlationId, value);
        }

        public static Result<TKey, TValue> Create(TKey resultId, TKey correlationId, IValueProvider<TValue> valueProvider)
        {
            return new Result<TKey, TValue>(resultId, correlationId, valueProvider.Value);
        }

        public static async Task<Result<TKey, TValue>> Create<TParams>(TKey resultId, TKey correlationId, IValueFactory<TValue> valueFactory, TParams parameters, CancellationToken cancellationToken)
        {
            return new Result<TKey, TValue>(resultId, correlationId, await valueFactory.CreateValueAsync(cancellationToken));
        }

        public TKey ResultId { get; }
        public TKey CorrelationId { get; }
        public TValue Value { get; }
    }
}
