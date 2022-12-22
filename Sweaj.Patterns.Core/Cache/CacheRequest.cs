using Ardalis.GuardClauses;
using System.Linq.Expressions;

namespace Sweaj.Patterns.Cache
{
    public sealed class CacheRequest
    {
        private CacheRequest(string cacheKey, ValueRetrievalMethod valueRetrievalMethod)
        {
            Ardalis.GuardClauses.Guard.Against.NullOrWhiteSpace(cacheKey);

            CacheKey = cacheKey;
            ValueRetrievalMethod = valueRetrievalMethod;
            RequestId = Guid.NewGuid();
        }

        public Guid RequestId { get; }
        public string CacheKey { get; }
        public ValueRetrievalMethod ValueRetrievalMethod { get; }

        public static CacheRequest Create([NotNull, ValidatedNotNull] string cacheKey, [NotNull] ValueRetrievalMethod valueRetrievalMethod = ValueRetrievalMethod.Undefined)
        {
            var request = new CacheRequest(cacheKey, valueRetrievalMethod);

            return request;
        }

        public static implicit operator CacheRequest([CanBeNull, NotNull] string cachekey)
        {
            return new CacheRequest(cachekey, ValueRetrievalMethod.Undefined);
        }
    }

    public interface ICacheKeyValidation
    {
        bool Validate(Expression<Func<CacheRequest, bool>> predicate);
    }
}
