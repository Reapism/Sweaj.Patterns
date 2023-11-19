namespace Sweaj.Patterns.Cache
{
    public static class CacheRequestExtensions
    {
        public static async Task<CacheStore<TValue>> GetFromCacheOnly<TValue>(this CacheKey cacheKey, CacheManagerBase cacheManager, CancellationToken cancellationToken = default)
        {
            var request = CacheValueRequest<TValue>.GetFromCacheOnly(cacheKey);
            return await cacheManager.ProcessWithValueAsync<TValue>(request, cancellationToken);
        }
    }
}
