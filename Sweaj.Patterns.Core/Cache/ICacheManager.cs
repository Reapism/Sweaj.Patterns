namespace Sweaj.Patterns.Cache
{
    public interface ICacheManager
    {
        Task Expire<T>(CacheRequest<T> cacheOptions);
        Task<CacheStore<T>> GetAsync<T>(CacheRequest<T> cacheOptions)
            where T : notnull;
        Task<CacheStore<T>> SetAsync<T>(CacheRequest<T> cacheOptions)
            where T : notnull;
    }
}
