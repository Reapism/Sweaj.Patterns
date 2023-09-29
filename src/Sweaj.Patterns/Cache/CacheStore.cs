using Sweaj.Patterns.Data;
using Sweaj.Patterns.Data.Services;
using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Cache
{
    public sealed class CacheStore<T> : IEmpty, IValueProvider<T>
    {
        private CacheStore(CacheRequest cacheRequest, ValueResultStatus status, T value)
        {
            Value = Guard.Against.Null<T>(value, nameof(value));
            CacheRequest = cacheRequest;
            Status = status;
        }

        private CacheStore(CacheRequest<T> cacheRequest, ValueResultStatus status, T value)
        {
            Value = Guard.Against.Null<T>(value, nameof(value));
            CacheRequest = CacheRequest.From(cacheRequest);
            Status = status;
        }

        public T Value { get; }
        public CacheRequest CacheRequest { get; }
        public ValueResultStatus Status { get; }
        public bool IsEmpty() => ValueResultStatus.Empty == Status;

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.Empty"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>A default value <see cref="CacheStore{T}"/> that has a 
        /// <see cref="ValueResultStatus.Empty"/> status.</returns>
        public static CacheStore<T> Empty()
        {
            return new CacheStore<T>(default(CacheRequest), ValueResultStatus.Empty, default);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.Empty"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>A default value <see cref="CacheStore{T}"/> that has a 
        /// <see cref="ValueResultStatus.Empty"/> status.</returns>
        public static CacheStore<T> FromRequestEmptyValue(CacheRequest<T> cacheRequest)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.Empty, default);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.Empty"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>A default value <see cref="CacheStore{T}"/> that has a 
        /// <see cref="ValueResultStatus.Empty"/> status.</returns>
        public static CacheStore<T> FromRequestEmptyValue(CacheRequest cacheRequest)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.Empty, default);
        }
        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.Cache"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cacheRequest"></param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueResultStatus.FromCache"/> status.</returns>
        public static CacheStore<T> FromCache(CacheRequest<T> cacheRequest, T value)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.Cache, value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CacheStore{T}"/> that represents a value obtained from a <see cref="ValueResultStatus.DataStore"/>.
        /// </summary>
        /// <param name="cacheRequest"></param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueResultStatus.DataStore"/> status.</returns>
        public static CacheStore<T> FromDataStore(CacheRequest<T> cacheRequest, T value)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.DataStore, value);
        }

        /// <summary>
        /// Returns a new <see cref="CacheStore{T}"/> and specifies that this value
        /// was unknowningly obtained via cache or datastore due to a third party request.
        /// </summary>
        /// <param name="cacheRequest"></param>
        /// <returns>A <see cref="CacheStore{T}"/> that has a <see cref="ValueResultStatus.Undefined"/> status.</returns>
        public static CacheStore<T> FromThirdParty(CacheRequest<T> cacheRequest, T value)
        {
            return new CacheStore<T>(cacheRequest, ValueResultStatus.ThirdParty, value);
        }

        public ValueStore<T> AsValueStore()
        {
            return ValueStore<T>.FromCache(this);
        }
    }
}
