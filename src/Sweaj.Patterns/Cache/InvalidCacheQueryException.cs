using Sweaj.Patterns.Exceptions;

namespace Sweaj.Patterns.Cache
{
    public sealed class InvalidCacheQueryException : PatternsException
    {
        public static readonly string DefaultInvalidCacheQueryMessageFormat = "The cache key [{0}] does not match the expected format {1}";
        public InvalidCacheQueryException(ValueRetrievalMethod valueRetrievalMethod)
            : base($"The {nameof(ValueRetrievalMethod)} value [{valueRetrievalMethod}] is not supported.")
        { }
    }

}
