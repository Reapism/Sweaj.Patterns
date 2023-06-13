namespace Sweaj.Patterns.Converters
{
    /// <summary>
    /// Represents a contract for converting an object from one type to another.
    /// </summary>
    /// <typeparam name="TValue">The source value.</typeparam>
    /// <typeparam name="TReturn">The destination value.</typeparam>
    public interface IConverter<TValue, TReturn>
    {
        TReturn Convert(TValue value);
    }

    /// <summary>
    /// Represents a contract for converting an object from one type to another asynchronously
    /// while optionally supporting cancellation.
    /// </summary>
    /// <typeparam name="TValue">The source value.</typeparam>
    /// <typeparam name="TReturn">The destination value.</typeparam>
    public interface IAsyncConverter<TValue, TReturn>
    {
        Task<TReturn> ConvertAsync(TValue value, CancellationToken cancellationToken = default);
    }
}
