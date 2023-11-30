using Sweaj.Patterns.Data.Entities;

namespace Sweaj.Patterns.Data.Repositories
{
    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IRepository"/>
    /// instance.
    /// </summary>
    public interface IRepositoryRetriever
    {
        /// <summary>
        /// Returns a <see cref="IRepository"/> instance.
        /// </summary>
        /// <returns></returns>
        IRepository GetRepository();
    }

    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IRepository{TEntity}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TEntity">An entity in an underlying store.</typeparam>
    public interface IRepositoryRetriever<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        /// <summary>
        /// Returns a <see cref="IRepository{TEntity}"/> instance.
        /// </summary>
        /// <returns></returns>
        IRepository<TEntity> GetRepository();
    }

    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IRepository{TKey,TEntity}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TKey ">An entity in an underlying store.</typeparam>
    /// <typeparam name="TEntity">An entity in an underlying store.</typeparam>
    public interface IRepositoryRetriever<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        /// <summary>
        /// Returns a <see cref="IRepository{TKey, TEntity}"/> instance.
        /// </summary>
        /// <returns></returns>
        IRepository<TKey, TEntity> GetRepository();
    }

    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IReadOnlyRepository"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TEntity">An entity in an underlying store.</typeparam>
    public interface IReadOnlyRepositoryRetriever
    {
        /// <summary>
        /// Returns a <see cref="IReadOnlyRepository"/> instance.
        /// </summary>
        /// <returns></returns>
        IReadOnlyRepository GetReadOnlyRepository();
    }

    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IReadOnlyRepositoryRetriever{TEntity}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TEntity">An entity in an underlying store.</typeparam>
    public interface IReadOnlyRepositoryRetriever<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        /// <summary>
        /// Returns a <see cref="IReadOnlyRepository{TEntity}"/> instance.
        /// </summary>
        /// <returns></returns>
        IReadOnlyRepository<TEntity> GetReadOnlyRepository();
    }

    /// <summary>
    /// Provides a mechanism for retrieving a <see cref="IReadOnlyRepositoryRetriever{TKey, TEntity}"/>
    /// instance.
    /// </summary>
    /// <typeparam name="TKey ">An entity in an underlying store.</typeparam>
    /// <typeparam name="TEntity">An entity in an underlying store.</typeparam>
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
