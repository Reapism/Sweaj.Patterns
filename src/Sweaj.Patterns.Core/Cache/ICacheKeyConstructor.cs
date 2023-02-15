namespace Sweaj.Patterns.Cache
{
    /// <summary>
    /// Optionally, provides a contract to a value specifying how to create a 
    /// <see cref="CacheKey"/> from itself.
    /// <para>
    /// This pattern will remove the need for devs to create a cachekey themselves.
    /// using its own values.
    /// </para>
    /// </summary>
    public interface ICacheKeyConstructor
    {
        /// <summary>
        /// Specifies how to create 
        /// </summary>
        /// <returns></returns>
        CacheKey CreateCacheKey();
    }
}
