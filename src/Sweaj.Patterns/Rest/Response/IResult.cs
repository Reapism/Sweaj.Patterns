using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Response
{
    public interface IResult<TKey> : ICorrelationIdProvider<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey ResultId { get; }
    }

    public interface IResult<TKey, TResult> : IResult<TKey>, IValueProvider<TResult>
        where TKey : IEquatable<TKey>
    {

    }

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
            ResultId = resultId;
            CorrelationId = correlationId;
            Value = Guard.Against.Null(value);
        }

        public static Result<TKey, TValue> Create(TKey resultId, TKey correlationId, IValueProvider<TValue> valueProvider)
        {
            return new Result<TKey, TValue>(resultId, correlationId, valueProvider.Value);
        }

        public static async Task<Result<TKey, TValue>> Create<TParams>(TKey resultId, TKey correlationId, IValueFactory<TValue> valueFactory, TParams parameters, CancellationToken cancellationToken)
        {
            return new Result<TKey, TValue>(resultId, correlationId, await valueFactory.CreateValueAsync(cancellationToken));
        }

        /// <summary>
        /// Create a minimally instantiated derived result.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TParams"></typeparam>
        /// <param name="resultId"></param>
        /// <param name="correlationId"></param>
        /// <param name="valueFactory"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TResult> Create<TResult, TParams>(TKey resultId, TKey correlationId, IValueFactory<TValue> valueFactory, TParams parameters, CancellationToken cancellationToken)
            where TResult : Result<TKey, TValue>
        {
            return (TResult) new Result<TKey, TValue>(resultId, correlationId, await valueFactory.CreateValueAsync(cancellationToken));
        }

        public TKey ResultId { get; }
        public TKey CorrelationId { get; }
        public TValue Value { get; }
    }
}
