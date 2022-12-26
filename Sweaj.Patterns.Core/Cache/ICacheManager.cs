using Microsoft.Extensions.Caching.Distributed;
using Sweaj.Patterns.Data.Entities;
using Sweaj.Patterns.Data.Services;

namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// Combines the IDataM
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataStoreManager<TEntity> : IDataStoreManager<Guid, TEntity>
        where TEntity : Entity
    {

    }

    public interface IDataStoreManager<TKey, TEntity>
        where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>, new()
    {
        IDataStore<TEntity> Get();
    }

    public interface ICacheManager
    {
        CacheStore<T> Transform<T>(CacheRequest cacheRequest);
    }

    public sealed class CacheManager : ICacheManager
    {
        private readonly IDistributedCache distributedCache;
        public CacheManager(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public Task<CacheStore<T>> TransformAsync<T>(CacheRequest cacheRequest)
        {
            Guard.Against.Null(cacheRequest, nameof(cacheRequest));

            switch (cacheRequest.ValueRetrievalMethod)
            {
                case ValueRetrievalMethod.SafeGet:
                    return SafeGet<T>(cacheRequest);
                case ValueRetrievalMethod.Get:
                    break;
                case ValueRetrievalMethod.Create:
                    break;
                case ValueRetrievalMethod.SafeCreate:
                    break;
                case ValueRetrievalMethod.Update:
                    break;
                case ValueRetrievalMethod.Refresh:
                    break;
                default:
                    throw new NotSupportedException("");
            }
        }

        private CacheStore<T> SafeGet<T>(CacheRequest<T> cacheRequest)
        {
            // value factory must be non null here
        }
    }
}
