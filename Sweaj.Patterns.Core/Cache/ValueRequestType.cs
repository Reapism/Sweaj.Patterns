﻿namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// Describes how to obtain the value for a given cache request.
    /// Each option is optimized for a particular behavior.
    /// </summary>
    public enum ValueRetrievalMethod
    {
        /// <summary>
        /// <see cref="SafeGet"/> attempts to retrieve the value from
        /// cache, and if its not available, then attempts to retrieve the value
        /// from the underlying
        /// datastore.
        /// </summary>
        SafeGet = 1,

        /// <summary>
        /// <see cref="Get"/> attempts to retrieve the value from
        /// cache, and if its not available, then nothing is returned.
        /// </summary>
        Get = 2,

        /// <summary>
        /// <see cref="Create"/> will add a new value into the cache that is just created in the
        /// underlying datastore.
        /// </summary>
        Create = 3,

        /// <summary>
        /// <see cref="CreateIfNotExists"/> will only create a cache value if the value
        /// exists in the datastore.
        /// </summary>
        SafeCreate = 4,

        /// <summary>
        /// <see cref="Update"/> will update the underlying cache value with a new value provided
        /// within the request.
        /// </summary>
        Update = 5,

        /// <summary>
        /// <see cref="Refresh"/> will understand the cache value wtu
        /// </summary>
        Refresh = 6,

        /// <summary>
        /// <see cref="Expire"/> will expire the cache from the cache system.
        /// </summary>
        Expire = 7

    }
}
