using Sweaj.Patterns.Data.Values;
using Sweaj.Patterns.Logging;

namespace Sweaj.Patterns.Converters
{
    /// <summary>
    /// Represents a contract for converting an object from one type to another.
    /// </summary>
    /// <typeparam name="TValue">The source value.</typeparam>
    /// <typeparam name="TReturn">The converted value.</typeparam>
    public interface IConverter
    {
        ConversionResult<TReturn> Convert<TValue, TReturn>(TValue value);
    }

    /// <summary>
    /// Represents a contract for converting an object from one type to another.
    /// </summary>
    /// <typeparam name="TValue">The source value.</typeparam>
    /// <typeparam name="TReturn">The converted value.</typeparam>
    public interface IConverter<TValue, TReturn>
    {
        ConversionResult<TReturn> Convert(TValue value);
    }

    /// <summary>
    /// Represents a contract for converting an object from one type to another asynchronously
    /// while optionally supporting cancellation.
    /// </summary>
    /// <typeparam name="TValue">The source value.</typeparam>
    /// <typeparam name="TReturn">The converted value.</typeparam>
    public interface IAsyncConverter<TValue, TReturn> 
        where TValue : IValueProvider<TReturn>
    {
        Task<ConversionResult<TReturn>> ConvertAsync(TValue value, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Represents a contract for converting an object from one type to another asynchronously
    /// while optionally supporting cancellation.
    /// </summary>
    /// <typeparam name="TValue">The source value.</typeparam>
    /// <typeparam name="TReturn">The converted value.</typeparam>
    public interface IAsyncConverter
    {
        Task<ConversionResult<TReturn>> ConvertAsync<TValue, TReturn>(TValue value, CancellationToken cancellationToken = default);
    }

    public sealed class AutoConverter
    {
        private readonly ILogger logger;

        public AutoConverter(ILogger logger)
        {
            this.logger = logger;
        }

        public TResult AutoConvert<TValue, TResult>(IConverter converter, TValue value, TResult result)
            where TValue : IValueProvider<TValue>
        {
            var conversionResult = converter.Convert<TValue, TResult>(value);
            if (conversionResult.IsSuccessful)
                return conversionResult.Value;

            logger.LogError("Auto conversion for {SourceType} to {TargetType}", typeof(TValue).FullName, typeof(TResult).FullName);
            return default;
        }
    }
}
