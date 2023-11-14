namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// A read-only cache request using a cacheKey to update or refresh *existing* cache.
    /// </summary>
    public sealed class CacheReadOnlyRequest : ICacheReadOnlyRequest
    {
        private CacheReadOnlyRequest(Guid requestId, CacheKey cacheKey, ValueRetrievalMethod cacheRetrievalMethod, CacheDurationOptions cacheDurationOptions)
        {
            CacheKey = cacheKey;
            ValueRetrievalMethod = cacheRetrievalMethod;

            RequestId = requestId;
            CacheDurationOptions = cacheDurationOptions;
        }

        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public ValueRetrievalMethod ValueRetrievalMethod { get; }
        public CacheDurationOptions? CacheDurationOptions { get; }

        public static CacheReadOnlyRequest From(ICacheReadOnlyRequest cacheRequest)
        {
            return new CacheReadOnlyRequest(cacheRequest.RequestId, cacheRequest.CacheKey, cacheRequest.ValueRetrievalMethod, cacheRequest.CacheDurationOptions);
        }

        public static CacheReadOnlyRequest Refresh(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, CacheDurationOptions cacheDurationOptions)
        {
            var request = new CacheReadOnlyRequest(Guid.NewGuid(), cacheKey, ValueRetrievalMethod.RefreshCacheOnly, cacheDurationOptions);
            return request;
        }

        public static CacheReadOnlyRequest Expire(
            [NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheReadOnlyRequest(Guid.NewGuid(), cacheKey, ValueRetrievalMethod.ExpireCacheOnly, null);
            return request;
        }
    }
}
