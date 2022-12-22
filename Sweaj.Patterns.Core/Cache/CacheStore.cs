using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Cache
{
    public sealed class CacheStore<T> : IEmpty<CacheStore<T>>
    {
        private CacheStore(CacheRequest cacheRequest, ValueRetrievalStatus status)
        {
            CacheRequest = cacheRequest;
            Status = status;
        }

        public CacheRequest CacheRequest { get; }
        public ValueRetrievalStatus Status { get; }

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueRetrievalStatus.Empty"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>A default value <see cref="CacheStore{T}"/> that has a 
        /// <see cref="ValueRetrievalStatus.Empty"/> status.</returns>
        public CacheStore<T> Empty()
        {
            return new CacheStore<T>(default, ValueRetrievalStatus.Empty);
        }

        public bool IsEmpty() => CacheRequest.Value is null; 

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueRetrievalStatus.DataStore"/>.
        /// </summary>
        /// <param name="value">The value retrieved.</param>
        /// <param name="cacheOptions">The options used to </param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueRetrievalStatus.DataStore"/> status.</returns>
        public static CacheStore<T> FromDataStore(CacheRequest<T> cacheOptions)
        {
            return new CacheStore<T>(cacheOptions, ValueRetrievalStatus.DataStore);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueRetrievalStatus.Cache"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cacheOptions"></param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueRetrievalStatus.FromCache"/> status.</returns>
        public static CacheStore<T> FromCache(CacheRequest<T> cacheOptions)
        {
            return new CacheStore<T>(cacheOptions, ValueRetrievalStatus.Cache);
        }

        /// <summary>
        /// Returns a new <see cref="CacheStore{T}"/> and specifies that this value
        /// was unknowningly obtained via cache or datastore due to a third party request.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cacheRequest"></param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueRetrievalStatus.Undefined"/> status.</returns>
        public static CacheStore<T> FromThirdParty(CacheRequest<T> cacheRequest)
        {
            return new CacheStore<T>(cacheRequest, ValueRetrievalStatus.Undefined);
        }
    }
}
