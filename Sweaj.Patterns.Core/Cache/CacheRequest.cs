using System.Linq.Expressions;

namespace Sweaj.Patterns.Cache
{
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

        public static CacheRequest<T> SafeGet<T>(
            [NotNull, ValidatedNotNull] CacheKey cacheKey, Func<Task<T>> valueFactory)
        {
            var request = new CacheRequest<T>(cacheKey, ValueRetrievalMethod.SafeGet, default, valueFactory);

            return request;
        }

        public static CacheRequest<T> Get<T>(
            [NotNull, ValidatedNotNull] CacheKey cacheKey)
        {
            var request = new CacheRequest<T>(cacheKey, ValueRetrievalMethod.Get, default, null);

            return request;
        }
    }

    public interface ICacheKeyValidation
    {
        bool Validate<TValue>(Expression<Func<CacheRequest<TValue>, bool>> predicate);
    }
}
