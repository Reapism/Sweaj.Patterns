using Sweaj.Patterns.Exceptions;

namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// Thrown when the cache key does not match the expected format.
    /// </summary>
    public sealed class InvalidCacheKeyException : PatternsException
    {
        public static readonly string DefaultInvalidCacheKeyMessageFormat = "The cache key [{0}] does not match the expected format {1}";
        public InvalidCacheKeyException(CacheKey cacheKey, string cacheKeyFormat)
            : base(string.Format(DefaultInvalidCacheKeyMessageFormat, cacheKey, cacheKeyFormat))
        { }

        public InvalidCacheKeyException(string cacheKey, string cacheKeyFormat)
            : base(string.Format(DefaultInvalidCacheKeyMessageFormat, cacheKey, cacheKeyFormat))
        { }
    }

}
