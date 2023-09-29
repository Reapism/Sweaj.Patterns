using Sweaj.Patterns.Data.Entities;
using System.Diagnostics;

namespace Sweaj.Patterns.Data.Services
{
    public class DataStore<TKey, TEntity> : IValueProvider<TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>
    {
        private DataStore(TEntity value, TimeSpan duration, DataRetrievalStatus dataRetrievalStatus)
        {
            Value = Guard.Against.Null(value, nameof(value));
            Duration = duration;
            Status = dataRetrievalStatus;
        }

        public static DataStore<TKey, TEntity> FromDatabase(
            Func<TEntity> getValueDelegate)
        {
            return From(getValueDelegate, DataRetrievalStatus.Database);
        }

        public static async Task<DataStore<TKey, TEntity>> FromDatabaseAsync(
            Func<Task<TEntity>> getValueDelegate)
        {
            return await FromAsync(getValueDelegate, DataRetrievalStatus.Database);
        }

        public static DataStore<TKey, TEntity> FromCache(
            Func<TEntity> getValueDelegate)
        {
            return From(getValueDelegate, DataRetrievalStatus.Cache);
        }

        public static async Task<DataStore<TKey, TEntity>> FromCacheAsync(
            Func<Task<TEntity>> getValueDelegate)
        {
            return await FromAsync(getValueDelegate, DataRetrievalStatus.Cache);
        }

        public static DataStore<TKey, TEntity> FromFile(
            Func<TEntity> getValueDelegate)
        {
            return From(getValueDelegate, DataRetrievalStatus.File);
        }

        public static async Task<DataStore<TKey, TEntity>> FromFileAsync(Func<Task<TEntity>> getValueDelegate)
        {
            return await FromAsync(getValueDelegate, DataRetrievalStatus.File);
        }

        public static DataStore<TKey, TEntity> FromWebResource(Func<TEntity> getValueDelegate)
        {
            return From(getValueDelegate, DataRetrievalStatus.WebResource);
        }

        public static async Task<DataStore<TKey, TEntity>> FromWebResourceAsync(Func<Task<TEntity>> getValueDelegate)
        {
            return await FromAsync(getValueDelegate, DataRetrievalStatus.WebResource);
        }

        private static DataStore<TKey, TEntity> From(
            Func<TEntity> getValueDelegate, DataRetrievalStatus dataRetrievalStatus)
        {
            var time = Stopwatch.StartNew();

            var value = getValueDelegate();

            time.Stop();
            var duration = time.Elapsed;

            var dataStore = new DataStore<TKey, TEntity>(value, duration, dataRetrievalStatus);

            return dataStore;
        }

        private static async Task<DataStore<TKey, TEntity>> FromAsync(
            Func<Task<TEntity>> getValueDelegate, DataRetrievalStatus dataRetrievalStatus)
        {
            var startTime = Stopwatch.GetTimestamp();

            var value = await getValueDelegate();

            var endTime = Stopwatch.GetTimestamp();
            var duration = Stopwatch.GetElapsedTime(startTime, endTime);

            var dataStore = new DataStore<TKey, TEntity>(value, duration, dataRetrievalStatus);

            return dataStore;
        }

        public TEntity Value { get; } = default(TEntity);

        public bool HasValue => Value is not null;
        public DataRetrievalStatus Status { get; }
        TimeSpan Duration { get; }

        public ValueStore<TEntity> AsValueStore()
        {
            return ValueStore<TEntity>.FromDataStore(this);
        }
    }
}
