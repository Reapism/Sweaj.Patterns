namespace Sweaj.Patterns.Cache
{
    public interface ICacheReadOnlyRequest
    {
        public Guid RequestId { get; }
        public CacheKey CacheKey { get; }
        public ValueRetrievalMethod ValueRetrievalMethod { get; }
        public CacheDurationOptions? CacheDurationOptions { get; }
    }
}
