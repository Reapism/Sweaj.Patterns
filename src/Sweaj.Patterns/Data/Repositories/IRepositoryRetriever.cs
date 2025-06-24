using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Entities;

namespace Sweaj.Patterns.Data.Repositories
{
    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IRepository{TEntity}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TEntity">An entity in an underlying store.</typeparam>
    [Trackable]
    public interface IRepositoryRetriever
    {
        /// <summary>
        /// Returns a <see cref="IRepository{TEntity}"/> instance.
        /// </summary>
        /// <returns></returns>
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : Entity, IAggregateRoot;
    }

    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IRepository{TKey,TEntity}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TKey ">An entity in an underlying store.</typeparam>
    /// <typeparam name="TEntity">An entity in an underlying store.</typeparam>
    [Trackable]
    public interface IRepositoryRetriever<TKey, TEntity>

    {
        /// <summary>
        /// Returns a <see cref="IRepository{TKey, TEntity}"/> instance.
        /// </summary>
        /// <returns></returns>
        IRepository<TKey, TEntity> GetRepository<TKey, TEntity>()
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;
    }

    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IReadOnlyRepositoryRetriever{TEntity}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TEntity">An entity in an underlying store.</typeparam>
    [Trackable]
    public interface IReadOnlyRepositoryRetriever<TEntity>
    {
        /// <summary>
        /// Returns a <see cref="IReadOnlyRepository{TEntity}"/> instance.
        /// </summary>
        /// <returns></returns>
        IReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>()
            where TEntity : Entity, IAggregateRoot;
    }

    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IReadOnlyRepositoryRetriever{TKey, TEntity}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TKey ">An entity in an underlying store.</typeparam>
    /// <typeparam name="TEntity">An entity in an underlying store.</typeparam>
    [Trackable]
    public interface IReadOnlyRepositoryRetriever<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        /// <summary>
        /// Returns a <see cref="IReadOnlyRepository{TKey, TEntity}"/> instance.
        /// </summary>
        /// <returns></returns>
        IReadOnlyRepository<TKey, TEntity> GetReadOnlyRepository();
    }
}
