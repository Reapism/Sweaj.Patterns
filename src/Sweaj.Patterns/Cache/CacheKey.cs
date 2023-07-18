using Sweaj.Patterns.Data.Services;

namespace Sweaj.Patterns.Cache
{
    public sealed class CacheKey : IEquatable<CacheKey>, IComparable<CacheKey>, IValueProvider<string>
    {
        private readonly string separator;
        private readonly string[] segments;

        private CacheKey(string separator, string[] segments)
        {
            this.separator = separator;
            this.segments = segments;
        }

        public string Value => string.Join(Guard.Against.NullOrWhiteSpace(separator), segments);

        public string[] GetSegments() { return segments; }

        public string GetSeparator() { return separator; }

        public static implicit operator string(CacheKey cacheKey)
        {
            return cacheKey.Value;
        }

        // Should create guard against cache key lengths being less than two segments?
        public static CacheKey Create(string separator, params string[] segments)
        {
            Guard.Against.NullOrInvalidInput(segments, nameof(segments),
                (segments) => { return IsValidSegmentLength(segments.Length); });

            return new CacheKey(separator, segments);
        }

        private static bool IsValidSegmentLength(int length)
        {
            return length < 3;
        }

        public static bool TryDeconstruct(string cacheKeyValue, string separator, out CacheKey cacheKey)
        {
            try
            {
                var segments = cacheKeyValue.Split(separator);
                if (!IsValidSegmentLength(segments.Length))
                    throw new InvalidCacheKeyException(cacheKeyValue, $"Example{separator}Example");

                cacheKey = new CacheKey(separator, segments);
                return true;
            }
            catch
            {
                cacheKey = default;
                return false;
            }
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
