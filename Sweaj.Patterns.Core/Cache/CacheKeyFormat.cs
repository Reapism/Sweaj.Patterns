
namespace Sweaj.Patterns.Cache
{
    public sealed class CacheOptions
    {
        public CacheOptions() { }

        public CacheKeyFormat CacheKeyFormat { get; }
        public CacheDurationOptions CacheDurationOptions { get; }
    }

    public sealed class CacheKeyFormat
    {
        private CacheKeyFormat(string cacheKeyFormat)
        {
            Value = Guard.Against.NullOrEmpty(cacheKeyFormat, nameof(cacheKeyFormat));
        }
        public string Value { get; }
        public static implicit operator CacheKeyFormat(string cacheKey)
        {
            return new CacheKeyFormat(cacheKey);
        }

        public override bool Equals(object? obj)
        {
            Guard.Against.Null(obj);
            if (obj is CacheKeyFormat cacheKeyFormat)
                return Value.Equals(cacheKeyFormat.Value);

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        public override string ToString()
        {
            return Value;
        }
    }
}
