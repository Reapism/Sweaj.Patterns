namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// Marks a type as being cachable by providing its cache key.
    /// </summary>
    public interface ICachable
    {
        CacheKey CacheKey { get; }
    }
}
