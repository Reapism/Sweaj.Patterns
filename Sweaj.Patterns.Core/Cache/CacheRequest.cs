using System.Linq.Expressions;

namespace Sweaj.Patterns.Cache
{
    public sealed class CacheRequest
    {
        private CacheRequest(CacheKey cacheKey, ValueRetrievalMethod valueRetrievalMethod)
        {
            CacheKey = cacheKey;
            ValueRetrievalMethod = valueRetrievalMethod;

            RequestId = Guid.NewGuid();
        }

        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public ValueRetrievalMethod ValueRetrievalMethod { get; }

        public static CacheRequest Create(CacheKey cacheKey, ValueRetrievalMethod valueRetrievalMethod)
        {
            return new CacheRequest(cacheKey, valueRetrievalMethod);
        }
    }

    public sealed class CacheRequest<TValue>
    {
        private CacheRequest(CacheKey cacheKey, ValueRetrievalMethod valueRetrievalMethod, TValue? value, Func<Task<TValue>>? valueFactory)
        {
            CacheKey = cacheKey;
            ValueRetrievalMethod = valueRetrievalMethod;

            RequestId = Guid.NewGuid();
            Value = value;
            ValueFactory = valueFactory;
        }

        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public ValueRetrievalMethod ValueRetrievalMethod { get; }

        public Func<Task<TValue>>? ValueFactory { get; }

        public TValue? Value { get; }
        public bool HasValueFactory()
        {
            return ValueRetrievalMethod is ValueRetrievalMethod.SafeGet or ValueRetrievalMethod.Update;
        }

        public static CacheRequest<TValue> SafeGet<TValue>(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, Func<Task<TValue>> valueFactory)
        {
            var request = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.SafeGet, default, valueFactory);

            return request;
        }

        public static CacheRequest<TValue> Get<TValue>(
            [NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.Get, default, null);

            return request;
        }

        public static CacheRequest<TValue> Set<TValue>(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, TValue value)
        {
            var request = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.Set, value, null);

            return request;
        }

        public static CacheRequest<TValue> SafeSet<TValue>(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, TValue value, Func<Task<TValue>> valueFactory)
        {
            var request = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.SafeSet, value, valueFactory);

            return request;
        }

        public static CacheRequest<TValue> Update<TValue>(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, TValue newValue)
        {
            var request = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.Update, newValue, null);

            return request;
        }

        public static CacheRequest<TValue> Refresh<TValue>(
            [NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.Refresh, default, null);

            return request;
        }

        public static CacheRequest<TValue> Expire<TValue>(
            [NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheRequest<TValue>(cacheKey, ValueRetrievalMethod.Expire, default, null);

            return request;
        }
    }

    public interface ICacheKeyValidation
    {
        bool Validate<TValue>(Expression<Func<CacheRequest<TValue>, bool>> predicate);
    }
}
