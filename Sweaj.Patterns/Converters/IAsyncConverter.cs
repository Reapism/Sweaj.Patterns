using Sweaj.Patterns.Data.Services;

namespace Sweaj.Patterns.Converters
{
    public interface IAsyncConverter<TValue, TReturn>
    {
        Task<TReturn> ConvertAsync(TValue value, CancellationToken cancellationToken = default);
    }

    public class ConversionResult<TResult> : IValueProvider<TResult>
    {
        private ConversionResult(TResult result, bool isSuccessful, string errorMessage = "")
        {
            IsSuccessful = isSuccessful;
            Value = isSuccessful ? Guard.Against.Null(result) : default;
            ErrorMessage = errorMessage;
        }
        public TResult? Value { get; }
        public bool IsSuccessful { get; }
        public string ErrorMessage { get; }

        public static ConversionResult<TResult> FromSuccessful(TResult result)
        {
            return new ConversionResult<TResult>(result, true);
        }

        public static ConversionResult<TResult> FromFailure(string errorMessage)
        {
            return new ConversionResult<TResult>(default, false, errorMessage);
        }
    }
}
