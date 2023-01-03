using System.Linq.Expressions;

namespace Sweaj.Patterns.Cache
{
    public sealed class CacheRequest
    {
        private CacheRequest(CacheKey cacheKey, ValueRetrievalMethod valueRetrievalMethod, CacheDurationOptions cacheDurationOptions)
        {
            CacheKey = cacheKey;
            ValueRetrievalMethod = valueRetrievalMethod;

            RequestId = Guid.NewGuid();
            CacheDurationOptions = cacheDurationOptions;
        }

        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public ValueRetrievalMethod ValueRetrievalMethod { get; }
        public CacheDurationOptions? CacheDurationOptions { get; }

        public static CacheRequest Create([NotNull, ValidatedNotNull] CacheKey cacheKey, ValueRetrievalMethod valueRetrievalMethod, CacheDurationOptions cacheDurationOptions)
        {
            var request = new CacheRequest(cacheKey, valueRetrievalMethod, cacheDurationOptions);
            return request;
        }

        public static CacheRequest Get([NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheRequest(cacheKey, ValueRetrievalMethod.Get, null);
            return request;
        }

        public static CacheRequest Refresh(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, CacheDurationOptions cacheDurationOptions)
        {
            var request = new CacheRequest(cacheKey, ValueRetrievalMethod.Refresh, cacheDurationOptions);
            return request;
        }

        public static CacheRequest Expire(
            [NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheRequest(cacheKey, ValueRetrievalMethod.Expire, null);
            return request;
        }
    }

    public sealed class CacheRequestValue<TValue>
    {
        private CacheRequestValue(CacheRequest cacheRequest, TValue? value, Func<Task<TValue>>? valueFactory)
        {
            Request = cacheRequest;
            Value = value;
            ValueFactory = valueFactory;
        }
        public CacheRequest Request { get; }

        public Func<Task<TValue>>? ValueFactory { get; }

        public TValue? Value { get; }
        public bool HasValueFactory()
        {
            return ValueFactory is not null;
        }

        public static CacheRequestValue<TValue> SafeGet(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, Func<Task<TValue>> valueFactory)
        {
            var request = CacheRequest.Get(cacheKey);
            var valueRequest = new CacheRequestValue<TValue>(request, default, valueFactory);

            return valueRequest;
        }

        public static CacheRequestValue<TValue> Set(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, TValue value, CacheDurationOptions cacheDurationOptions)
        {
            var request = CacheRequest.Create(cacheKey, ValueRetrievalMethod.Set, cacheDurationOptions);
            var valueRequest = new CacheRequestValue<TValue>(request, value, null);

            return valueRequest;
        }

        public static CacheRequestValue<TValue> SafeSet(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, TValue value, Func<Task<TValue>> valueFactory, CacheDurationOptions cacheDurationOptions)
        {
            var request = CacheRequest.Create(cacheKey, ValueRetrievalMethod.SafeSet, cacheDurationOptions);
            var valueRequest = new CacheRequestValue<TValue>(request, value, valueFactory);

            return valueRequest;
        }

        public static CacheRequestValue<TValue> Update(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, TValue newValue, CacheDurationOptions cacheDurationOptions)
        {
            var request = CacheRequest.Create(cacheKey, ValueRetrievalMethod.Update, cacheDurationOptions);
            var valueRequest = new CacheRequestValue<TValue>(request, newValue, null);

            return valueRequest;
        }
    }
}
