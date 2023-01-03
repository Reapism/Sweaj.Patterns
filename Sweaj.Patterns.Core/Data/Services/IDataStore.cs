using Sweaj.Patterns.Data.Entities;
using System.Diagnostics;

namespace Sweaj.Patterns.Data.Services
{
    public interface IDataStore<TEntity> : IDataStore<Guid, TEntity>
        where TEntity : Entity
    {
    }


    public interface IDataStore<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>
    {
        TEntity? Value { get; }
        bool HasValue { get; }
        DataRetrievalStatus Status { get; }
    }

    public class DataStore<TKey, TEntity> : IDataStore<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>
    {
        private DataStore(TEntity? value, TimeSpan duration, DataRetrievalStatus dataRetrievalStatus)
        {
            Value = value;
            Duration = duration;
            Status = dataRetrievalStatus;
        }

        public static DataStore<TKey, TEntity> FromDatabase(
            Func<TEntity> getValueDelegate)
        {
            return FromValue(getValueDelegate, DataRetrievalStatus.Database);
        }

        public static async Task<DataStore<TKey, TEntity>> FromDatabaseAsync(
            Func<Task<TEntity>> getValueDelegate)
        {
            return await FromValueAsync(getValueDelegate, DataRetrievalStatus.Database);
        }

        public static DataStore<TKey, TEntity> FromCache(
            Func<TEntity> getValueDelegate)
        {
            return FromValue(getValueDelegate, DataRetrievalStatus.Cache);
        }

        public static async Task<DataStore<TKey, TEntity>> FromCacheAsync(
            Func<Task<TEntity>> getValueDelegate)
        {
            return await FromValueAsync(getValueDelegate, DataRetrievalStatus.Cache);
        }

        public static DataStore<TKey, TEntity> FromFile(
            Func<TEntity> getValueDelegate)
        {
            return FromValue(getValueDelegate, DataRetrievalStatus.File);
        }

        public static async Task<DataStore<TKey, TEntity>> FromFileAsync(Func<Task<TEntity>> getValueDelegate)
        {
            return await FromValueAsync(getValueDelegate, DataRetrievalStatus.File);
        }

        public static DataStore<TKey, TEntity> FromWebResource(Func<TEntity> getValueDelegate)
        {
            return FromValue(getValueDelegate, DataRetrievalStatus.WebResource);
        }

        public static async Task<DataStore<TKey, TEntity>> FromWebResourceAsync(Func<Task<TEntity>> getValueDelegate)
        {
            return await FromValueAsync(getValueDelegate, DataRetrievalStatus.WebResource);
        }

        private static DataStore<TKey, TEntity> FromValue(
            Func<TEntity> getValueDelegate, DataRetrievalStatus dataRetrievalStatus)
        {
            var time = Stopwatch.StartNew();

            var value = getValueDelegate();

            time.Stop();
            var duration = time.Elapsed;

            var status = GetDataRetrievalStatusOrEmpty(value, dataRetrievalStatus);
            var dataStore = new DataStore<TKey, TEntity>(value, duration, status);

            return dataStore;
        }

        private static async Task<DataStore<TKey, TEntity>> FromValueAsync(
            Func<Task<TEntity>> getValueDelegate, DataRetrievalStatus dataRetrievalStatus)
        {
            var startTime = Stopwatch.GetTimestamp();

            var value = await getValueDelegate();

            var endTime = Stopwatch.GetTimestamp();
            var duration = Stopwatch.GetElapsedTime(startTime, endTime);

            var status = GetDataRetrievalStatusOrEmpty(value, dataRetrievalStatus);
            var dataStore = new DataStore<TKey, TEntity>(value, duration, status);

            return dataStore;
        }

        private static DataRetrievalStatus GetDataRetrievalStatusOrEmpty(TEntity? entity, DataRetrievalStatus dataRetrievalStatus)
        {
            var entityExists = entity is not null;

            if (!entityExists)
            {
                DataRetrievalStatus emptyStatus = (DataRetrievalStatus)(int)dataRetrievalStatus++;
                return emptyStatus;
            }

            return dataRetrievalStatus;
        }

        public TEntity Value { get; } = default(TEntity);

        public bool HasValue => Value is not null;
        public DataRetrievalStatus Status { get; }
        TimeSpan Duration { get; }
    }
}
