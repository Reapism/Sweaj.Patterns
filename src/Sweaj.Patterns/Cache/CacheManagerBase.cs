using Sweaj.Patterns.Serialization.Json;

namespace Sweaj.Patterns.Cache
{
    public abstract class CacheManagerBase
    {
        public abstract Task ProcessAsync<TValue>(CacheReadOnlyRequest cacheRequest, CancellationToken cancellationToken = default);
        public abstract Task<CacheStore<TValue>> ProcessWithValueAsync<TValue>(CacheValueRequest<TValue> cacheRequest, IJsonSerializer<TValue> serializer, CancellationToken cancellationToken = default);
    }
}
