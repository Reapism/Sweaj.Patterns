using Sweaj.Patterns.NullObject;
using System.Net.Http.Headers;

namespace Sweaj.Patterns.Cache
{
    public sealed class CacheStore<T> : IEmpty<CacheStore<T>>
    {
        private CacheStore(CacheRequest<T> cacheRequest, ValueResultStatus status)
        {
            CacheRequest = Guard.Against.Null(cacheRequest, nameof(cacheRequest));
            Status = status;
        }

        public CacheRequest<T> CacheRequest { get; }
        public ValueResultStatus Status { get; }
        public bool IsEmpty() => CacheRequest is null;

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

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.Cache"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cacheRequest"></param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueResultStatus.FromCache"/> status.</returns>
        public static CacheStore<T> FromCache(CacheRequest<T> cacheRequest)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.Cache);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.DataStore"/>.
        /// </summary>
        /// <param name="value">The value retrieved.</param>
        /// <param name="cacheRequest">The options used to </param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueResultStatus.DataStore"/> status.</returns>
        public static CacheStore<T> FromDataStore(CacheRequest<T> cacheRequest)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.DataStore);
        }

        /// <summary>
        /// Returns a new <see cref="CacheStore{T}"/> and specifies that this value
        /// was unknowningly obtained via cache or datastore due to a third party request.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cacheRequest"></param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueResultStatus.Undefined"/> status.</returns>
        public static CacheStore<T> FromThirdParty(CacheRequest<T> cacheRequest)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.ThirdParty);
        }
    }
}
