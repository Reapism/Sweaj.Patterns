using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Cache
{
    public sealed class CacheKey : IEquatable<CacheKey>, IComparable<CacheKey>, IValueProvider<string>
    {
        private const int MinSegmentLength = 3;
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

        public static CacheKey Create(string separator, params string[] segments)
        {
            Guard.Against.NullOrInvalidInput(segments, nameof(segments),
                (segments) => { return IsValidSegmentLength(segments.Length); });

            return new CacheKey(separator, segments);
        }

        private static bool IsValidSegmentLength(int length)
        {
            return length < MinSegmentLength;
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
