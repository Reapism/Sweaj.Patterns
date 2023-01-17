namespace Sweaj.Patterns.Cache
{
    public sealed class CacheKey : IEquatable<CacheKey>, IComparable<CacheKey>
    {
        private CacheKey(string cacheKey)
        {
            Value = cacheKey;
        }

        public string Value { get; }

        public static implicit operator string(CacheKey cacheKey)
        {
            return cacheKey.Value;
        }

        public static explicit operator CacheKey(string cacheKey)
        {
            return new CacheKey(cacheKey);
        }

        // Should create guard against cache key lengths being less than two segments?
        public static CacheKey Create(string separator, params string[] segments)
        {
            Guard.Against.InvalidInput(segments, nameof(segments),
                (segments) => { return segments.Length >= 2; });
            var value = string.Join(Guard.Against.NullOrWhiteSpace(separator), Guard.Against.NullOrEmpty(segments));

            return new CacheKey(value);
        }

        public CacheStore<TValue> IntoFetchStore<TValue>()
        {
            CacheStore<TValue>.FromCache(CacheRequest.Get(this));
        }

        public override bool Equals(object? obj)
        {
            Guard.Against.Null(obj);
            if (obj is CacheKey cacheKey)
            {
                return Value.Equals(cacheKey.Value);
            }

            return false;
        }

        public bool Equals(CacheKey? other)
        {
            return Value.Equals(other?.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        public int CompareTo(CacheKey? other)
        {
            return Value.CompareTo(other?.Value);
        }
    }
}
