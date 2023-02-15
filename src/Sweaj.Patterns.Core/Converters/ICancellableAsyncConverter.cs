namespace Sweaj.Patterns.Converters
{
    public interface ICancellableAsyncConverter<TValue, TResult> : IAsyncConverter<TValue, TResult>
    {
        Task<ConversionResult<TResult>> ConvertAsync(TValue value, CancellationToken cancellationToken);
    }
}
