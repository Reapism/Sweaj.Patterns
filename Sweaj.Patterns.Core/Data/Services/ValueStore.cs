using Sweaj.Patterns.Cache;
using Sweaj.Patterns.Data.Entities;
using Sweaj.Patterns.Mapping;

namespace Sweaj.Patterns.Data.Services
{
    public sealed class ValueStore<TValue>
        where TValue : class
    {
        public TValue Value { get; }
        public ValueResultStatus ValueResultStatus { get; }
        private ValueStore(TValue value, ValueResultStatus valueResultStatus)
        {
            Value = Guard.Against.Null(value, nameof(value)); ;
            ValueResultStatus = valueResultStatus;
        }

        public static ValueStore<TValue> FromCache(CacheStore<TValue> cacheStore)
        {
            return new ValueStore<TValue>(cacheStore.Value, ValueResultStatus.Cache);
        }

        public static ValueStore<TValue> FromDataStore<TKey, TEntity>(IEntityToValueMapper<TEntity, TValue> mapper, DataStore<TKey, TEntity> dataStore)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>, new()
        {
            var value = mapper.Convert(dataStore.Value);
            return new ValueStore<TValue>(value, ValueResultStatus.DataStore);
        }
    }
}
