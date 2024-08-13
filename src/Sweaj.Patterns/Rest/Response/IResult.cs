using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Response
{
    [Trackable]
    public interface IResult<TValue> : IValueProvider<TValue?>
    { }

    [Trackable]
    public class Result<TValue> : IResult<TValue?>
    {
        /// <summary>
        /// Initializes a result instance using the default constructor from
        /// </summary>
        protected Result(TValue? value)
        {
            ResultId = Guid.NewGuid();
            Value = value;
        }

        public static Result<TValue?> Create(TValue value)
        {
            return new Result<TValue?>(value);
        }

        public static Result<TValue?> Create(IValueProvider<TValue> valueProvider)
        {
            return new Result<TValue?>(valueProvider.Value);
        }

        public static async Task<Result<TValue?>> Create<TParams>(IValueFactory<TValue> valueFactory, TParams parameters, CancellationToken cancellationToken)
        {
            return new Result<TValue?>(await valueFactory.CreateValueAsync(cancellationToken));
        }

        public Guid ResultId { get; }
        public TValue? Value { get; }
    }
}
