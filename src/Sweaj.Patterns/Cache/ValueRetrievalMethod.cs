namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// Describes how the value is obtained for a given cache request.
    /// </summary>
    public enum ValueRetrievalMethod
    {
        /// <summary>
        /// <see cref="GetFromCacheOrFactory"/> attempts to retrieve the value from
        /// cache, and if its not available, then attempts to retrieve the value
        /// from the underlying datastore.
        /// </summary>
        GetFromCacheOrFactory = 1,

        /// <summary>
        /// <see cref="GetFromCacheOnly"/> attempts to retrieve the value from
        /// cache, and if its not available, then nothing is returned.
        /// </summary>
        GetFromCacheOnly = 2,

        /// <summary>
        /// <see cref="SetCacheOnly"/> will add a new value into the cache that is just created in the
        /// underlying datastore.
        /// </summary>
        SetCacheOnly = 3,

        /// <summary>
        /// <see cref="SetCacheOrCreateFactoryThenSetCache"/> will only set a cache value if the value
        /// exists in the datastore.
        /// </summary>
        SetCacheOrCreateFactoryThenSetCache = 4,

        /// <summary>
        /// <see cref="UpdateCacheOnly"/> will update the underlying cache value with a new value provided
        /// within the request.
        /// </summary>
        UpdateCacheOnly = 5,

        /// <summary>
        /// <see cref="RefreshCacheOnly"/> will refresh the cache duration for an existing cache if it exists, otherwise nothing.
        /// </summary>
        RefreshCacheOnly = 6,

        /// <summary>
        /// <see cref="ExpireCacheOnly"/> will expire the cache from the cache system if it exists, does nothing otherwise.
        /// </summary>
        ExpireCacheOnly = 7,

    }
}
