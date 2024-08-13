using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Serialization.Json;

namespace Sweaj.Patterns.Cache
{
    [Trackable]
    public abstract class CacheManagerBase
    {
        public abstract Task ProcessAsync<TValue>(CacheReadOnlyRequest cacheRequest, CancellationToken cancellationToken = default);
        public abstract Task<CacheStore<TValue>> ProcessWithValueAsync<TValue>(CacheValueRequest<TValue> cacheRequest, IJsonSerializer serializer, CancellationToken cancellationToken = default);
    }
}
