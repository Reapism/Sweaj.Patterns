using Ardalis.GuardClauses;
using Microsoft.Extensions.Caching.Memory;
using Sweaj.Patterns.Cache;

namespace Sweaj.Patterns.Tests.Cache
{
    public sealed class RandomValue : ICachable
    {
        public RandomValue(string value)
        {
            Value = Guard.Against.NullOrWhiteSpace(value);
        }
        public string Value { get; }

        public CacheKey CacheKey => CacheKey.Create("|", nameof(RandomValue), Value);

    }

    public class CacheIntegrationTests
    {
        [Fact]
        public void CacheWorksE2E()
        {
            //var randomValue = new RandomValue(Random.Shared.Next().ToString());
            //CacheManagerBase cacheManager = new DistributedCacheManager(new DistributedCacheManager(MemoryCac))
            //var randomValueStore = CacheRequest<RandomValue>.SetCacheOnly(randomValue, CacheDurationOptions.FromRelativeToNowExpiration(TimeSpan.FromMinutes(1))).Get();



        }



    }
}