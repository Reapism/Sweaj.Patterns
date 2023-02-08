﻿namespace Sweaj.Patterns.Converters
{
    public interface IAsyncConverter<TValue, TReturn>
    {
        Task<TReturn> ConvertAsync(TValue value, CancellationToken cancellationToken = default);
    }
}