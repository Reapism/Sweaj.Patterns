using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Sweaj.Patterns.Cache
{

    public abstract class CacheManagerBase
    {
        public abstract Task ProcessAsync<TValue>(CacheRequest cacheRequest, CancellationToken cancellationToken = default);
        public abstract Task<CacheStore<TValue>> ProcessWithValueAsync<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default);
    }

    public sealed class MemoryCacheManager : CacheManagerBase
    {
        public override Task ProcessAsync<TValue>(CacheRequest cacheRequest, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<CacheStore<TValue>> ProcessWithValueAsync<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class DistributedCacheManager : CacheManagerBase
    {
        private static readonly string InvalidValueRetrievalMethodInTransformingTheCacheRequest = $"The following {nameof(ValueRetrievalMethod)} is not supported.";

        private readonly IDistributedCache distributedCache;

        public DistributedCacheManager(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public override async Task ProcessAsync<TValue>([NotNull] CacheRequest cacheRequest, CancellationToken cancellationToken = default)
        {
            switch (cacheRequest.ValueRetrievalMethod)
            {
                case ValueRetrievalMethod.RefreshCacheOnly:
                    await RefreshCacheOnly<TValue>(cacheRequest.CacheKey, cancellationToken);
                    break;
                case ValueRetrievalMethod.ExpireCacheOnly:
                    await ExpireCacheOnly<TValue>(cacheRequest.CacheKey, cancellationToken);
                    break;
                default:
                    throw new ApplicationException(InvalidValueRetrievalMethodInTransformingTheCacheRequest);
            }
        }

        public override async Task<CacheStore<TValue>> ProcessWithValueAsync<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(cacheRequest, nameof(cacheRequest));

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
                    throw new ApplicationException("Impossible case");

            }
        }

        private async Task<CacheStore<TValue>> GetFromCacheOnly<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            var bytes = await distributedCache.GetAsync(cacheRequest.CacheKey, cancellationToken);
            return CacheStore<TValue>.FromCache(cacheRequest);
        }

        private async Task<CacheStore<TValue>> GetFromCacheOrFactory<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            var bytes = await distributedCache.GetAsync(cacheRequest.CacheKey, cancellationToken);
            return CacheStore<TValue>.FromCache(cacheRequest);
        }

        private async Task<CacheStore<TValue>> SetCacheOnly<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            await SetCacheOnlyInternal(cacheRequest.CacheKey, cacheRequest.Value, cancellationToken);
            return CacheStore<TValue>.FromCache(cacheRequest);
        }

        private async Task<CacheStore<TValue>> SetCacheOrCreateFactoryThenSetCache<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken = default)
        {
            var value = cacheRequest.Value;
            if (value is null)
            {
                value = await cacheRequest.ValueFactory();
                if (value is null)
                {
                    return CacheStore<TValue>.Empty();
                }
            }

            return await SetCacheOnlyInternal(cacheRequest, value, FromCacheDurationOptions(cacheRequest.) cancellationToken);
        }

        private async Task SetCacheOnlyInternal<TValue>(CacheKey cacheKey, TValue? value, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(value);
            var valueInBytes = Encoding.UTF8.GetBytes(json);

            await distributedCache.SetAsync(cacheKey, valueInBytes, cancellationToken);
        }

        private async Task<CacheStore<TValue>> UpdateCacheOnly<TValue>(CacheRequest<TValue> cacheRequest, CancellationToken cancellationToken)
        {
            await distributedCache.RemoveAsync(cacheRequest.CacheKey, cancellationToken);
            return await SetCacheOnlyInternal<TValue>(cacheRequest.CacheKey, cacheRequest.Value, FromCacheDurationOptions(cacheRequest.CacheDurationOptions) cancellationToken)
        }

        private async Task RefreshCacheOnly<TValue>(CacheKey cacheKey, CancellationToken cancellationToken)
        {
            await distributedCache.RefreshAsync(cacheKey, cancellationToken);
        }


        private async Task ExpireCacheOnly<TValue>(CacheKey cacheKey, CancellationToken cancellationToken)
        {
            await distributedCache.RemoveAsync(cacheKey, cancellationToken);

            CacheStore<TValue>.
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
