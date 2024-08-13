using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// Marks a type as being cachable by providing its cache key.
    /// </summary>
    [Trackable]
    public interface ICachable
    {
        CacheKey CacheKey { get; }
    }
}
