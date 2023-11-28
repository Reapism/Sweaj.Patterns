using Sweaj.Patterns.Serialization.Json;

namespace Sweaj.Patterns.Cache
{
    public static class CacheRequestExtensions
    {
        public static async Task<CacheStore<TValue>> GetFromCacheOnly<TValue>(this CacheKey cacheKey, CacheManagerBase cacheManager, IJsonSerializer jsonSerializer, CancellationToken cancellationToken = default)
        {
            var request = CacheValueRequest<TValue>.GetFromCacheOnly(cacheKey);
            return await cacheManager.ProcessWithValueAsync(request, jsonSerializer, cancellationToken);
        }
    }
}
