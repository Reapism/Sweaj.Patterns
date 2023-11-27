using Sweaj.Patterns.Data.Entities;
using System.Diagnostics;

namespace Sweaj.Patterns.Data.Values
{
    public class DataStore<TKey, TEntity> : IValueProvider<TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>
    {
        private DataStore(TEntity value, TimeSpan duration, ValueResultStatus valueResultStatus)
        {
            Value = Guard.Against.Null(value, nameof(value));
            Duration = duration;
            Status = valueResultStatus;
        }

        public TEntity Value { get; } = default;

        public bool HasValue => Value is not null;
        public ValueResultStatus Status { get; }
        TimeSpan Duration { get; }

        public static DataStore<TKey, TEntity> FromDatastore(
            Func<TEntity> getValueDelegate)
        {
            return From(getValueDelegate, ValueResultStatus.ValueFactoryFromDataStore);
        }

        public static async Task<DataStore<TKey, TEntity>> FromDatabaseAsync(
            Func<Task<TEntity>> getValueDelegate)
        {
            return await FromAsync(getValueDelegate, ValueResultStatus.ValueFactoryFromDataStore);
        }

        public static DataStore<TKey, TEntity> FromCache(
            Func<TEntity> getValueDelegate)
        {
            return From(getValueDelegate, ValueResultStatus.Cache);
        }

        public static async Task<DataStore<TKey, TEntity>> FromCacheAsync(
            Func<Task<TEntity>> getValueDelegate)
        {
            return await FromAsync(getValueDelegate, ValueResultStatus.ValueFactoryFromCache);
        }

        public static DataStore<TKey, TEntity> FromFile(
            Func<TEntity> getValueDelegate)
        {
            return From(getValueDelegate, ValueResultStatus.File);
        }

        public static async Task<DataStore<TKey, TEntity>> FromFileAsync(Func<Task<TEntity>> getValueDelegate)
        {
            return await FromAsync(getValueDelegate, ValueResultStatus.File);
        }

        public static DataStore<TKey, TEntity> FromWebResource(Func<TEntity> getValueDelegate)
        {
            return From(getValueDelegate, ValueResultStatus.WebResource);
        }

        public static async Task<DataStore<TKey, TEntity>> FromWebResourceAsync(Func<Task<TEntity>> getValueDelegate)
        {
            return await FromAsync(getValueDelegate, ValueResultStatus.WebResource);
        }

        private static DataStore<TKey, TEntity> From(
            Func<TEntity> getValueDelegate, ValueResultStatus dataRetrievalStatus)
        {
            var time = Stopwatch.StartNew();

            var value = getValueDelegate();

            time.Stop();
            var duration = time.Elapsed;

            var dataStore = new DataStore<TKey, TEntity>(value, duration, dataRetrievalStatus);

            return dataStore;
        }

        private static async Task<DataStore<TKey, TEntity>> FromAsync(
            Func<Task<TEntity>> getValueDelegate, ValueResultStatus dataRetrievalStatus)
        {
            var startTime = Stopwatch.GetTimestamp();

            var value = await getValueDelegate();

            var endTime = Stopwatch.GetTimestamp();
            var duration = Stopwatch.GetElapsedTime(startTime, endTime);

            var dataStore = new DataStore<TKey, TEntity>(value, duration, dataRetrievalStatus);

            return dataStore;
        }

        public ValueStore<TEntity> AsValueStore()
        {
            return ValueStore<TEntity>.FromDataStore(this);
        }
    }
}
