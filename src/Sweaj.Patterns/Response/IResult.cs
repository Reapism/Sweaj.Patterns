using Sweaj.Patterns.Data.Services;

namespace Sweaj.Patterns.Response
{
    public interface IResult<TKey>
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
        /// Initializes a result instance using the default constructor from <
        /// </summary>
        public Result([NotNull] IValueProvider<TValue> value) : this(new(), value)
        {
        }
        public Result(TKey resultId, [NotNull] IValueProvider<TValue> value)
        {
            ResultId = resultId;
            Value = value.Value;
        }

        public TKey ResultId { get; }

        public TValue Value { get; }
    }
}
