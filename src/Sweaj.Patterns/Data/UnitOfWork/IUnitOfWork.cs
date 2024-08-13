using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Entities;
using Sweaj.Patterns.Data.Repositories;

namespace Sweaj.Patterns.Data.UnitOfWork
{
    /// <summary>
    /// Interface for unit of work pattern.
    /// </summary>
    [Trackable]
    public interface IUnitOfWork : IRepositoryRetriever
    {
        /// <summary>
        /// Commits all changes made in the unit of work asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous commit operation.</returns>
        Task<int> CommitAsync();
    }

    /// <summary>
    /// Generic interface for unit of work pattern.
    /// </summary>
    /// <typeparam name="TEntity">An entity in an underlying store.The type of entities that the unit of work manages.</typeparam>
    [Trackable]
    public interface IUnitOfWork<TEntity> : IRepositoryRetriever<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        /// <summary>
        /// Commits all changes made in the unit of work asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous commit operation.</returns>
        Task<int> CommitAsync();
    }

    /// <summary>
    /// Generic interface for unit of work pattern.
    /// </summary>
    /// <typeparam name="TKey">The type of key of the entities that the unit of work manages.</typeparam>
    /// <typeparam name="TEntity">An entity in an underlying store.The type of entities that the unit of work manages.</typeparam>
    [Trackable]
    public interface IUnitOfWork<TKey, TEntity> : IRepositoryRetriever<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        /// <summary>
        /// Commits all changes made in the unit of work asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous commit operation.</returns>
        Task<int> CommitAsync();
    }
}
