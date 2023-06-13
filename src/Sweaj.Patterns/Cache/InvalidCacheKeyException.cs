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
            : base($"The cachekey [{cacheKey}] is not in the expected cache key format [{cacheKeyFormat}]")
        { }
    }

    public sealed class InvalidCacheQueryException : PatternsException 
    {
        public static readonly string DefaultInvalidCacheQueryMessageFormat = "The cache key [{0}] does not match the expected format {1}";
        public InvalidCacheKeyException(ValueRetrievalMethod valueRetrievalMethod, string cacheKeyFormat)
            : base($"The {nameof(ValueRetrievalMethod)} value [{valueRetrievalMethod}] is not supported.")
        { }
    }

}
