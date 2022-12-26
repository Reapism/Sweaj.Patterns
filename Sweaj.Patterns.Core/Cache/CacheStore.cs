using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Cache
{
    public sealed class CacheStore<T> : IEmpty<CacheStore<T>>
    {
        private CacheStore(CacheRequest cacheRequest, ValueResultStatus status)
        {
            CacheRequest = cacheRequest;
            Status = status;
        }

        public CacheRequest CacheRequest { get; }
        public ValueResultStatus Status { get; }

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.Empty"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>A default value <see cref="CacheStore{T}"/> that has a 
        /// <see cref="ValueResultStatus.Empty"/> status.</returns>
        public CacheStore<T> Empty()
        {
            return new CacheStore<T>(default, ValueResultStatus.Empty);
        }

        public bool IsEmpty() => CacheRequest is null; 

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.DataStore"/>.
        /// </summary>
        /// <param name="value">The value retrieved.</param>
        /// <param name="cacheRequest">The options used to </param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueResultStatus.DataStore"/> status.</returns>
        public static CacheStore<T> FromDataStore(CacheRequest cacheRequest)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.DataStore);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.Cache"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cacheRequest"></param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueResultStatus.FromCache"/> status.</returns>
        public static CacheStore<T> FromCache(CacheRequest cacheRequest)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.Cache);
        }

        /// <summary>
        /// Returns a new <see cref="CacheStore{T}"/> and specifies that this value
        /// was unknowningly obtained via cache or datastore due to a third party request.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cacheRequest"></param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueResultStatus.Undefined"/> status.</returns>
        public static CacheStore<T> FromThirdParty(CacheRequest cacheRequest)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.Undefined);
        }
    }
}
