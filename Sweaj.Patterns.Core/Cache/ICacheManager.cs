//using Microsoft.Extensions.Caching.Distributed;
//using System.Text;
//using System.Text.Json;

//namespace Sweaj.Patterns.Cache
//{
//    public interface ICacheManager
//    {
//        Task<CacheStore<TValue>> TransformAsync<TValue>(CacheRequestValue<TValue> cacheRequest, CancellationToken cancellationToken = default);
//    }

//    public sealed class CacheManager : ICacheManager
//    {
//        private readonly IDistributedCache distributedCache;
//        private static readonly string InvalidValueRetrievalMethodInTransformingTheCacheRequest = $"The following {nameof(ValueRetrievalMethod)} is not supported.";

//        public CacheManager(IDistributedCache distributedCache)
//        {
//            this.distributedCache = distributedCache;
//        }

//        public async Task<CacheStore<TValue>> TransformAsync<TValue>(CacheRequestValue<TValue> cacheRequest, CancellationToken cancellationToken = default)
//        {
//            Guard.Against.Null(cacheRequest, nameof(cacheRequest));

//            switch (cacheRequest.Request.ValueRetrievalMethod)
//            {
//                case ValueRetrievalMethod.SafeGet:
//                    return await SafeGet(cacheRequest, cancellationToken);
//                case ValueRetrievalMethod.Get:
//                    return await Get<TValue>(cacheRequest, cancellationToken);
//                case ValueRetrievalMethod.Set:
//                    return await Set<TValue>(cacheRequest.CacheKey, cacheRequest.Value, cancellationToken);
//                case ValueRetrievalMethod.SafeSet:
//                    return await SafeSet<TValue>(cacheRequest, cancellationToken);
//                case ValueRetrievalMethod.Update:
//                    return await Update<TValue>(cacheRequest, cancellationToken);
//                case ValueRetrievalMethod.Refresh:
//                    return await Refresh<TValue>(cacheRequest, cancellationToken);
//                default:
//                    throw new ApplicationException("Impossible case");

//            }
//        }

//        private async Task<CacheStore<TValue>> SafeGet<TValue>(CacheRequestValue<TValue> cacheRequest, CancellationToken cancellationToken)
//        {
//            var bytes = await distributedCache.GetAsync(cacheKey, cancellationToken);
//            var @string = Encoding.UTF8.GetString(bytes);
//            var cacheExists = bytes is not null;

//            if (cacheExists)
//            {
//                CacheStore<TValue>.FromCache(cac)
//            }

//        }

//        private async Task<CacheStore<TValue>> Get<TValue>(CacheRequest cacheRequest, CancellationToken cancellationToken)
//        {
//            var bytes = await distributedCache.GetAsync(cacheRequest.CacheKey, cancellationToken);

//        }

//        private async Task<CacheStore<TValue>> Set<TValue>(CacheRequestValue<TValue> cacheRequest, CancellationToken cancellationToken)
//        {
//            var cacher
//            var bytes = await distributedCache.SetAsync(cacheRequest, cancellationToken);
//        }

//        private async Task<CacheStore<TValue>> SafeSet<TValue>(CacheKey cacheKey, CancellationToken cancellationToken)
//        {
//            var bytes = await distributedCache.GetAsync(cacheKey, cancellationToken);
//        }

//        private async Task<CacheStore<TValue>> Update<TValue>(CacheKey cacheKey, CancellationToken cancellationToken)
//        {
//            var bytes = await distributedCache.GetAsync(cacheKey, cancellationToken);
//        }

//        private async Task<CacheStore<TValue>> Refresh<TValue>(CacheKey cacheKey, CancellationToken cancellationToken)
//        {
//            var bytes = await distributedCache.GetAsync(cacheKey, cancellationToken);
//        }

//        private string GetString(byte[] bytes)
//        {
//            var str = Encoding.UTF8.GetString(bytes);

//        }

//        private TValue GetValue<TValue>(string json)
//        {
//            var value = JsonSerializer.Deserialize<TValue>(json, JsonSerializerOptions.Default)
//        }
//    }
//}
