using Sweaj.Patterns.Data.Services;

namespace Sweaj.Patterns.Cache
{
    public interface ICacheRequest
    {
        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public ValueRetrievalMethod ValueRetrievalMethod { get; }
        public CacheDurationOptions? CacheDurationOptions { get; }
    }

    public sealed class CacheRequest : ICacheRequest
    {
        private CacheRequest(Guid requestId, CacheKey cacheKey, ValueRetrievalMethod cacheRetrievalMethod, CacheDurationOptions cacheDurationOptions)
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

        public static CacheRequest From(ICacheRequest cacheRequest)
        {
            return new CacheRequest(cacheRequest.RequestId, cacheRequest.CacheKey, cacheRequest.ValueRetrievalMethod, cacheRequest.CacheDurationOptions);
        }

        public static CacheRequest RefreshCacheOnly(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, CacheDurationOptions cacheDurationOptions)
        {
            var request = new CacheRequest(Guid.NewGuid(), cacheKey, ValueRetrievalMethod.RefreshCacheOnly, cacheDurationOptions);
            return request;
        }

        public static CacheRequest ExpireCacheOnly(
            [NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheRequest(Guid.NewGuid(), cacheKey, ValueRetrievalMethod.ExpireCacheOnly, null);
            return request;
        }
    }

    public sealed class CacheRequest<TValue> : ICacheRequest, IValueProvider<TValue?>
    {
        private CacheRequest(
            CacheKey cacheKey,
            ValueRetrievalMethod valueRetrievalMethod,
            TValue? value,
            CacheDurationOptions? cacheDurationOptions,
            Func<Task<TValue>>? valueFactory)
        {
            RequestId = Guid.NewGuid();
            CacheKey = cacheKey;
            ValueRetrievalMethod = valueRetrievalMethod;
            CacheDurationOptions = cacheDurationOptions;
            Value = value;
            ValueFactory = valueFactory;
        }

        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public ValueRetrievalMethod ValueRetrievalMethod { get; }
        public CacheDurationOptions? CacheDurationOptions { get; }

        public Func<Task<TValue>>? ValueFactory { get; }

        public TValue? Value { get; }

        public static CacheRequest<TValue> GetFromCacheOnly(CacheKey cacheKey)
        {
            var valueRequest = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.GetFromCacheOnly, default, null, null);

            return valueRequest;
        }
        public static CacheRequest<TValue> GetFromCacheOrFactory(CacheKey cacheKey, [NotNull, ValidatedNotNull] Func<Task<TValue>> valueFactory)
        {
            var valueRequest = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.GetFromCacheOrFactory, default, null, valueFactory);

            return valueRequest;
        }

        public static CacheRequest<TValue> SetCacheOnly(CacheKey cacheKey, [NotNull, ValidatedNotNull] TValue value, [NotNull, ValidatedNotNull] CacheDurationOptions cacheDurationOptions)
        {
            var valueRequest = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.SetCacheOnly, Guard.Against.Null(value), Guard.Against.Null(cacheDurationOptions), null);

            return valueRequest;
        }

        public static CacheRequest<TValue> SetCacheOrCreateFactoryThenSetCache(CacheKey cacheKey, [CanBeNull] TValue? value, [NotNull, ValidatedNotNull] CacheDurationOptions cacheDurationOptions, [NotNull, ValidatedNotNull] Func<Task<TValue>> valueFactory)
        {
            var valueRequest = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.SetCacheOrCreateFactoryThenSetCache, value, Guard.Against.Null(cacheDurationOptions), Guard.Against.Null(valueFactory));

            return valueRequest;
        }

        public static CacheRequest<TValue> UpdateCacheOnly(CacheKey cacheKey, [NotNull, ValidatedNotNull] TValue newValue, [CanBeNull] CacheDurationOptions? cacheDurationOptions)
        {
            var valueRequest = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.UpdateCacheOnly, Guard.Against.Null(newValue), cacheDurationOptions, null);

            return valueRequest;
        }
    }
}
