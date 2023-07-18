using Microsoft.Extensions.Caching.Distributed;
using Sweaj.Patterns.Serialization.Json;
using System.Text;
using System.Text.Json;

namespace Sweaj.Patterns.Cache
{
    public abstract class CacheManagerBase
    {
        public abstract Task ProcessAsync<TValue>(CacheRequest cacheRequest, CancellationToken cancellationToken = default);
        public abstract Task<CacheStore<TValue>> ProcessWithValueAsync<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default);
    }

    public sealed class DistributedCacheManager : CacheManagerBase
    {
        private static readonly string InvalidValueRetrievalMethodInTransformingTheCacheRequest = $"The following {nameof(ValueRetrievalMethod)} is not supported.";

        private readonly IDistributedCache distributedCache;
        private readonly IJsonSerializer jsonSerializer;

        public DistributedCacheManager(IDistributedCache distributedCache, IJsonSerializer jsonSerializer)
        {
            this.distributedCache = distributedCache;
            this.jsonSerializer = jsonSerializer;
        }

        public override async Task ProcessAsync<TValue>([NotNull] CacheRequest cacheRequest, CancellationToken cancellationToken = default)
        {
            switch (cacheRequest.ValueRetrievalMethod)
            {
                case ValueRetrievalMethod.RefreshCacheOnly:
                    await RefreshCacheOnly<TValue>(cacheRequest, cancellationToken);
                    break;
                case ValueRetrievalMethod.ExpireCacheOnly:
                    await ExpireCacheOnly<TValue>(cacheRequest, cancellationToken);
                    break;
                default:
                    throw new ApplicationException(InvalidValueRetrievalMethodInTransformingTheCacheRequest);
            }
        }

        public override async Task<CacheStore<TValue>> ProcessWithValueAsync<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(cacheRequest, nameof(cacheRequest));

            // TODO refactor these methods below to not use a CacheRequest in the params and instead only use what is needed from
            // cacherequest in the params to avoid the Severity	Code CS8604  Possible null reference argument for parameter ''

            switch (cacheRequest.ValueRetrievalMethod)
            {
                case ValueRetrievalMethod.GetFromCacheOrFactory:
                    return await GetFromCacheOnly(cacheRequest, cancellationToken);
                case ValueRetrievalMethod.GetFromCacheOnly:
                    return await GetFromCacheOrFactory<TValue>(cacheRequest, cancellationToken);
                case ValueRetrievalMethod.SetCacheOnly:
                    return await SetCacheOnly<TValue>(cacheRequest, cancellationToken);
                case ValueRetrievalMethod.SetCacheOrCreateFactoryThenSetCache:
                    return await SetCacheOrCreateFactoryThenSetCache<TValue>(cacheRequest, cancellationToken);
                case ValueRetrievalMethod.UpdateCacheOnly:
                    return await UpdateCacheOnly<TValue>(cacheRequest, cancellationToken);
                default:
                    throw new InvalidCacheQueryException(cacheRequest.ValueRetrievalMethod);

            }
        }

        private async Task<CacheStore<TValue>> GetFromCacheOnly<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            var bytes = await distributedCache.GetAsync(cacheRequest.CacheKey, cancellationToken);
            if (bytes is null)
                return CacheStore<TValue>.Empty();

            var json = Encoding.UTF8.GetString(bytes);
            var deserializedValue = jsonSerializer.Deserialize<TValue>(json);

            if (deserializedValue is null)
                return CacheStore<TValue>.Empty();

            return CacheStore<TValue>.FromCache(cacheRequest, deserializedValue);
        }

        private async Task<CacheStore<TValue>> GetFromCacheOrFactory<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            var bytes = await distributedCache.GetAsync(cacheRequest.CacheKey, cancellationToken);
            if (bytes is null)
                return CacheStore<TValue>.Empty();

            var json = Encoding.UTF8.GetString(bytes);
            var deserializedValue = jsonSerializer.Deserialize<TValue>(json);

            if (deserializedValue is not null)
                return CacheStore<TValue>.FromCache(cacheRequest, deserializedValue);

            // Get value from factory.
            var valueFromFactory = await cacheRequest.ValueFactory();

            if (valueFromFactory is null)
                return CacheStore<TValue>.Empty();

            return CacheStore<TValue>.FromCache(cacheRequest, valueFromFactory);
        }

        private async Task<CacheStore<TValue>> SetCacheOnly<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            await SetCacheOnlyInternal(cacheRequest.CacheKey, cacheRequest.Value, FromCacheDurationOptions(cacheRequest.CacheDurationOptions), cancellationToken);

            return CacheStore<TValue>.FromCache(cacheRequest, cacheRequest.Value);
        }

        private async Task<CacheStore<TValue>> SetCacheOrCreateFactoryThenSetCache<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            // Get value provided or attempt to use create factory to get the value
            var value = (cacheRequest.Value is not null)
                ? cacheRequest.Value
                : (cacheRequest.ValueFactory is not null)
                    ? await cacheRequest.ValueFactory()
                    : default;

            // If value is null, we are empty in all cases.
            if (value is null)
            {
                return CacheStore<TValue>.FromRequestEmptyValue(cacheRequest);
            }

            await SetCacheOnlyInternal(cacheRequest.CacheKey, value, FromCacheDurationOptions(cacheRequest.CacheDurationOptions), cancellationToken);

            return CacheStore<TValue>.FromCache(cacheRequest, value);
        }

        private async Task SetCacheOnlyInternal<TValue>(CacheKey cacheKey, TValue value, DistributedCacheEntryOptions distributedCacheEntryOptions, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(value);
            var valueInBytes = Encoding.UTF8.GetBytes(json);

            await distributedCache.SetAsync(cacheKey, valueInBytes, distributedCacheEntryOptions, cancellationToken);
        }

        private async Task<CacheStore<TValue>> UpdateCacheOnly<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken)
        {
            await distributedCache.RemoveAsync(cacheRequest.CacheKey, cancellationToken);
            await SetCacheOnlyInternal<TValue>(cacheRequest.CacheKey, cacheRequest.Value, FromCacheDurationOptions(cacheRequest.CacheDurationOptions), cancellationToken);

            return CacheStore<TValue>.FromCache(cacheRequest, cacheRequest.Value);

        }

        private async Task<CacheStore<TValue>> RefreshCacheOnly<TValue>(CacheRequest cacheRequest, CancellationToken cancellationToken)
        {
            await distributedCache.RefreshAsync(cacheRequest.CacheKey, cancellationToken);

            return CacheStore<TValue>.FromRequestEmptyValue(cacheRequest);
        }


        private async Task<CacheStore<TValue>> ExpireCacheOnly<TValue>(CacheRequest cacheRequest, CancellationToken cancellationToken)
        {
            await distributedCache.RemoveAsync(cacheRequest.CacheKey, cancellationToken);

            return CacheStore<TValue>.FromRequestEmptyValue(cacheRequest);
        }

        private DistributedCacheEntryOptions FromCacheDurationOptions(CacheDurationOptions cacheDurationOptions)
        {
            return new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = cacheDurationOptions.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = cacheDurationOptions.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = cacheDurationOptions.SlidingExpiration
            };
        }
    }
}
