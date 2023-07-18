using Sweaj.Patterns.Cache;
using Sweaj.Patterns.Data.Entities;
using Sweaj.Patterns.Mapping;
using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Data.Services
{
    public sealed class ValueStore<TValue> : IEmpty
    {
        public TValue Value { get; }
        public ValueResultStatus ValueResultStatus { get; }
        private ValueStore(TValue value, ValueResultStatus valueResultStatus)
        {
            Value = value;
            ValueResultStatus = valueResultStatus;
        }

        public static ValueStore<TValue> FromEmpty()
        {
            return new ValueStore<TValue>(default, ValueResultStatus.Empty);
        }

        public static ValueStore<TValue> FromValue([NotNull, ValidatedNotNull] TValue value)
        {
            return new ValueStore<TValue>(Guard.Against.Null(value), ValueResultStatus.Value);
        }

        public static ValueStore<TValue> FromCache([NotNull, ValidatedNotNull] CacheStore<TValue> cacheStore)
        {
            return new ValueStore<TValue>(Guard.Against.Null(Guard.Against.Null(cacheStore).Value), ValueResultStatus.Cache);
        }

        public static ValueStore<TEntity> FromDataStore<TKey, TEntity>(DataStore<TKey, TEntity> dataStore)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>, new()
        {
            return new ValueStore<TEntity>(dataStore.Value, ValueResultStatus.DataStore);
        }

        public static ValueStore<TValue> FromDataStore<TEntity>(IEntityToValueMapper<TEntity, TValue> mapper, DataStore<Guid, TEntity> dataStore)
            where TEntity : Entity
        {
            var value = mapper.Convert(dataStore.Value);
            return new ValueStore<TValue>(value, ValueResultStatus.DataStore);
        }

        public bool IsEmpty()
        {
            return ValueResultStatus == ValueResultStatus.Empty;
        }
    }
}
