
namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// Encapsulates cachable behavior for a value.
    /// </summary>
    /// <typeparam name="TValue">The value to cache.</typeparam>
    /// <typeparam name="TParameter">An object representing all the values needed to construct a unique cache key.</typeparam>
    public abstract class CachableValueBase<TValue, TParameter>
        where TValue : class
        where TParameter : class
    {
        // Overridable default members

        protected const string DefaultCacheKeySeparator = "|||";
        protected virtual string CacheKeySeparator { get; } = DefaultCacheKeySeparator;
        protected virtual void AdditionalCacheKeyValidations(CacheKey cacheKey, TParameter parameters)
        { }

        // Must implement own version.
        protected abstract CacheKeyFormat CacheKeyFormat { get; init; }
        protected abstract string[] GetOrderedCacheKeySegments(TParameter parameters);

        public void ValidateCacheKey(TParameter parameters)
        {
            var segments = GetOrderedCacheKeySegments(parameters);
            var cacheKey = ConstructCacheKey(segments);

            if (segments.Length < 2)
            {
                throw new InvalidCacheKeyException(cacheKey, CacheKeyFormat);
            }

            AdditionalCacheKeyValidations(cacheKey, parameters);
        }

        protected CacheKey ConstructCacheKey(string[] segments)
        {
            var cacheKey = (CacheKey)string.Join(CacheKeySeparator, segments);
            return cacheKey;
        }

        protected string[] DeconstructCacheKey(CacheKey cacheKey)
        {
            return cacheKey.Value.Split(CacheKeySeparator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
