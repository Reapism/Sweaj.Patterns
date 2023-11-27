using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// A cache request that uses a value to either create, or modify existing cache
    /// using a value.
    /// </summary>
    /// <typeparam name="TValue">The value of the cache.</typeparam>
    public sealed class CacheValueRequest<TValue> : ICacheReadOnlyRequest, IValueProvider<TValue?>
    {
        private CacheValueRequest(
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

        public static CacheValueRequest<TValue> GetFromCacheOnly(CacheKey cacheKey)
        {
            var valueRequest = new CacheValueRequest<TValue>(cacheKey, ValueRetrievalMethod.GetFromCacheOnly, default, null, null);

            return valueRequest;
        }
        public static CacheValueRequest<TValue> GetFromCacheOrFactory(CacheKey cacheKey, [NotNull, ValidatedNotNull] Func<Task<TValue>> valueFactory)
        {
            var valueRequest = new CacheValueRequest<TValue>(cacheKey, ValueRetrievalMethod.GetFromCacheOrFactory, default, null, valueFactory);

            return valueRequest;
        }

        public static CacheValueRequest<TValue> SetCacheOnly(CacheKey cacheKey, [NotNull, ValidatedNotNull] TValue value, [NotNull, ValidatedNotNull] CacheDurationOptions cacheDurationOptions)
        {
            var valueRequest = new CacheValueRequest<TValue>(cacheKey, ValueRetrievalMethod.SetCacheOnly, Guard.Against.Null(value), Guard.Against.Null(cacheDurationOptions), null);

            return valueRequest;
        }

        public static CacheValueRequest<TValue> SetCacheOrCreateFactoryThenSetCache(CacheKey cacheKey, [CanBeNull] TValue? value, [NotNull, ValidatedNotNull] CacheDurationOptions cacheDurationOptions, [NotNull, ValidatedNotNull] Func<Task<TValue>> valueFactory)
        {
            var valueRequest = new CacheValueRequest<TValue>(cacheKey, ValueRetrievalMethod.SetCacheOrCreateFactoryThenSetCache, value, Guard.Against.Null(cacheDurationOptions), Guard.Against.Null(valueFactory));

            return valueRequest;
        }

        public static CacheValueRequest<TValue> UpdateCacheOnly(CacheKey cacheKey, [NotNull, ValidatedNotNull] TValue newValue, [CanBeNull] CacheDurationOptions? cacheDurationOptions)
        {
            var valueRequest = new CacheValueRequest<TValue>(cacheKey, ValueRetrievalMethod.UpdateCacheOnly, Guard.Against.Null(newValue), cacheDurationOptions, null);

            return valueRequest;
        }
    }
}
