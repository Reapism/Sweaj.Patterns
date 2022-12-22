using Sweaj.Patterns.Data.Entities;
using System.Diagnostics;

namespace Sweaj.Patterns.Data.Services
{
    public interface IDataStore<TEntity>
        where TEntity : Entity
    {
        TEntity? Value { get; }
        bool IsReadOnly { get; }
        bool HasValue { get; }
        DataRetrievalStatus Status { get; }
    }


    public interface IDataStore<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>
    {
        TEntity? Value { get; }
        bool IsReadOnly { get; }
        bool HasValue { get; }
        DataRetrievalStatus Status { get; }
    }

    public class DataStore<TKey, TEntity> : IDataStore<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>
    {
        private DataStore(TEntity? value, bool isReadOnly, TimeSpan duration, DataRetrievalStatus dataRetrievalStatus)
        {
            Value = value;
            IsReadOnly = isReadOnly;
            Duration = duration;
            Status = dataRetrievalStatus;
        }

        public static DataStore<TKey, TEntity> FromDatabase(
            Func<TEntity> getValueDelegate, bool isReadOnly)
        {
            return FromValue(getValueDelegate, isReadOnly, DataRetrievalStatus.Database);
        }

        public static async Task<DataStore<TKey, TEntity>> FromDatabaseAsync(
            Func<Task<TEntity>> getValueDelegate, bool isReadOnly)
        {
            return await FromValueAsync(getValueDelegate, isReadOnly, DataRetrievalStatus.Database);
        }

        public static DataStore<TKey, TEntity> FromCache(
            Func<TEntity> getValueDelegate, bool isReadOnly)
        {
            return FromValue(getValueDelegate, isReadOnly, DataRetrievalStatus.Cache);
        }

        public static async Task<DataStore<TKey, TEntity>> FromCacheAsync(
            Func<Task<TEntity>> getValueDelegate, bool isReadOnly)
        {
            return await FromValueAsync(getValueDelegate, isReadOnly, DataRetrievalStatus.Cache);
        }

        public static DataStore<TKey, TEntity> FromFile(
            Func<TEntity> getValueDelegate, bool isReadOnly)
        {
            return FromValue(getValueDelegate, isReadOnly, DataRetrievalStatus.File);
        }

        public static async Task<DataStore<TKey, TEntity>> FromFileAsync(Func<Task<TEntity>> getValueDelegate, bool isReadOnly)
        {
            return await FromValueAsync(getValueDelegate, isReadOnly, DataRetrievalStatus.File);
        }

        public static DataStore<TKey, TEntity> FromWebResource(Func<TEntity> getValueDelegate, bool isReadOnly)
        {
            return FromValue(getValueDelegate, isReadOnly, DataRetrievalStatus.WebResource);
        }

        public static async Task<DataStore<TKey, TEntity>> FromWebResourceAsync(Func<Task<TEntity>> getValueDelegate, bool isReadOnly)
        {
            return await FromValueAsync(getValueDelegate, isReadOnly, DataRetrievalStatus.WebResource);
        }

        private static DataStore<TKey, TEntity> FromValue(
            Func<TEntity> getValueDelegate, bool isReadOnly, DataRetrievalStatus dataRetrievalStatus)
        {
            var time = Stopwatch.StartNew();

            var value = getValueDelegate();

            time.Stop();
            var duration = time.Elapsed;

            var status = GetDataRetrievalStatusOrEmpty(value, dataRetrievalStatus);
            var dataStore = new DataStore<TKey, TEntity>(value, isReadOnly, duration, status);

            return dataStore;
        }

        private static async Task<DataStore<TKey, TEntity>> FromValueAsync(
            Func<Task<TEntity>> getValueDelegate, bool isReadOnly, DataRetrievalStatus dataRetrievalStatus)
        {
            var time = Stopwatch.StartNew();

            var value = await getValueDelegate();

            time.Stop();
            var duration = time.Elapsed;

            var status = GetDataRetrievalStatusOrEmpty(value, dataRetrievalStatus);
            var dataStore = new DataStore<TKey, TEntity>(value, isReadOnly, duration, status);

            return dataStore;
        }

        private static DataRetrievalStatus GetDataRetrievalStatusOrEmpty(TEntity? entity, DataRetrievalStatus dataRetrievalStatus)
        {
            var entityExists = entity is not null;
            var returnStatus = entityExists ? dataRetrievalStatus : (DataRetrievalStatus)(int)dataRetrievalStatus++;

            return returnStatus;
        }

        public TEntity? Value { get; }
        public bool IsReadOnly { get; }

        public bool HasValue => Value is not null;
        public DataRetrievalStatus Status { get; }
        TimeSpan Duration { get; }
    }
}
