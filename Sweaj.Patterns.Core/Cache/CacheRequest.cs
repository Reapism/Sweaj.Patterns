using Sweaj.Patterns.Data.Services;
using System.Linq.Expressions;

namespace Sweaj.Patterns.Cache
{
    public interface ICacheRequest
    {
        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public CacheRetrievalMethod CacheRetrievalMethod { get; }
        public CacheDurationOptions? CacheDurationOptions { get; }
    }

    public sealed class CacheRequest : ICacheRequest
    {
        private CacheRequest(CacheKey cacheKey, CacheRetrievalMethod cacheRetrievalMethod, CacheDurationOptions cacheDurationOptions)
        {
            CacheKey = cacheKey;
            CacheRetrievalMethod = cacheRetrievalMethod;

            RequestId = Guid.NewGuid();
            CacheDurationOptions = cacheDurationOptions;
        }

        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public CacheRetrievalMethod CacheRetrievalMethod { get; }
        public CacheDurationOptions? CacheDurationOptions { get; }

        public static CacheRequest Get([NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheRequest(cacheKey, CacheRetrievalMethod.Get, null);
            return request;
        }

        public static CacheRequest Refresh(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, CacheDurationOptions cacheDurationOptions)
        {
            var request = new CacheRequest(cacheKey, CacheRetrievalMethod.Refresh, cacheDurationOptions);
            return request;
        }

        public static CacheRequest Expire(
            [NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheRequest(cacheKey, CacheRetrievalMethod.Expire, null);
            return request;
        }
    }

    public sealed class CacheRequest<TValue> : IValueProvider<TValue>
    {
        private CacheRequest(
            CacheKey cacheKey,
            CacheRetrievalMethod cacheRetrievalMethod,
            CacheDurationOptions cacheDurationOptions,
            TValue? value,
            Func<Task<TValue>>? valueFactory)
        {
            RequestId = Guid.NewGuid();
            CacheKey = cacheKey;
            CacheRetrievalMethod = cacheRetrievalMethod;
            CacheDurationOptions = cacheDurationOptions;
            Value = value;
            ValueFactory = valueFactory;
        }
        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public CacheRetrievalMethod CacheRetrievalMethod { get; }
        public CacheDurationOptions? CacheDurationOptions { get; }

        public Func<Task<TValue>>? ValueFactory { get; }

        public TValue? Value { get; }

        public bool HasValueFactory()
        {
            return ValueFactory is not null;
        }

        public static CacheRequest<TValue> SafeGet(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, [NotNull, ValidatedNotNull] Func<Task<TValue>> valueFactory)
        {
            var request = CacheRequest.Get(cacheKey);
            var valueRequest = new CacheRequest<TValue>(request, default, valueFactory);

            return valueRequest;
        }

        public static CacheRequest<TValue> Set(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, TValue value, CacheDurationOptions cacheDurationOptions)
        {
            var request = CacheRequest(cacheKey, CacheRetrievalMethod.Set, cacheDurationOptions);
            var valueRequest = new CacheRequest<TValue>(request, value, null);

            return valueRequest;
        }

        public static CacheRequest<TValue> SafeSet(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, TValue value, Func<Task<TValue>> valueFactory, CacheDurationOptions cacheDurationOptions)
        {
            var request = CacheRequest.Create(cacheKey, CacheRetrievalMethod.SafeSet, cacheDurationOptions);
            var valueRequest = new CacheRequest<TValue>(request, value, valueFactory);

            return valueRequest;
        }

        public static CacheRequest<TValue> Update(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, TValue newValue, CacheDurationOptions cacheDurationOptions)
        {
            var request = CacheRequest.Create(cacheKey, CacheRetrievalMethod.Update, cacheDurationOptions);
            var valueRequest = new CacheRequest<TValue>(request, newValue, null);

            return valueRequest;
        }
    }
}
