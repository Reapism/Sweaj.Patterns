namespace Sweaj.Patterns.Cache
{
    public sealed class CacheKey
    {
        private CacheKey(string cacheKey)
        {
            Guard.Against.NullOrEmpty(cacheKey);
            Value = cacheKey; 
        }

        public string Value { get; }
        public static implicit operator CacheKey(string cacheKey)
        {
            return new CacheKey(cacheKey);
        }

        public override bool Equals(object? obj)
        {
            Guard.Against.Null(obj);
            if (obj is CacheKey cacheKey)
                return Value.Equals(cacheKey.Value);

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
