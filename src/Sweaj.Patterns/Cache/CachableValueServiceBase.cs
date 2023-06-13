namespace Sweaj.Patterns.Cache
{
    //TODO convert to interface to make this more flexible.
    // All functionality can be converted extension methods
    /// <summary>
    /// Encapsulates cacheable behavior for a value.
    /// </summary>
    /// <typeparam name="TValue">The value to cache.</typeparam>
    /// <typeparam name="TParameters">An object representing all the values needed to construct a unique cache key.</typeparam>
    public abstract class CacheableValueServiceBase<TValue, TParameters>
        where TValue : class
        where TParameters : class
    {
        // Overridable default members

        protected const string DefaultCacheKeySeparator = "|||";
        protected virtual string CacheKeySeparator { get; } = DefaultCacheKeySeparator;
        protected virtual void AdditionalCacheKeyValidations(CacheKey cacheKey, TParameters parameters)
        { }

        protected abstract string[] GetOrderedCacheKeySegments(TParameters parameters);

        protected abstract string CacheKeyFormat { get; }

        /// <summary>
        /// Intended to be called in the during object initialization of the derived type.
        /// <para>Intended to throw exceptions as these validations are fatal and should 
        /// prevent the cache key from being created read etc. since it does not meet the criteria.</para>
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="InvalidCacheKeyException"></exception>
        public void ValidateCacheKey(TParameters parameters)
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
