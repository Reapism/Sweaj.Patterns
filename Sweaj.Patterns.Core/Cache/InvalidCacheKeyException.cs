namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// Thrown when the cache key does not match the expected format.
    /// </summary>
    public sealed class InvalidCacheKeyException : Exception
    {
        public static readonly string DefaultInvalidCacheKeyMessageFormat = "The cache key [{0}] does not match the expected format {1}";
        public InvalidCacheKeyException(CacheKey cacheKey, CacheKeyFormat cacheKeyFormat)
            : base($"The cachekey [{cacheKey}] is not in the expected cache key format [{expectedCacheKeyFormat}]")
        { }
    }
}
