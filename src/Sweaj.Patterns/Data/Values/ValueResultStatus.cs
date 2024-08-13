namespace Sweaj.Patterns.Data.Values
{
    /// <summary>
    /// Represents how a value was retrieved.
    /// </summary>
    public enum ValueResultStatus : byte
    {
        /// <summary>
        /// An empty value was retrieved.
        /// </summary>
        Empty = 0,
        /// <summary>
        /// A value constructed not from an external resource.
        /// </summary>
        Value = 1,
        /// <summary>
        /// A value retrieved from cache.
        /// </summary>
        Cache = 2,
        /// <summary>
        /// A value retrieved from a data store.
        /// </summary>
        DataStore = 3,
        /// <summary>
        /// A value retrieved from a web resource.
        /// </summary>
        WebResource = 4,
        ValueFactoryFromCache = 5,
        ValueFactoryFromDataStore = 6,
        File = 7
    }
}
