using Sweaj.Patterns.Data.Entities;

namespace Sweaj.Patterns.Data
{
    /// <summary>
    /// Contract for CRUDing at generic method level.
    /// <para>Using this as a dependency allows you to reuse
    /// this dependency and querying multiple T's.</para>
    /// </summary>
    public interface IRepository : IReadOnlyRepository
    {
        /// <summary>
        /// Asynchronously adds a new entity to the repository.
        /// </summary>
        /// <typeparam name="TKey">The type of the entity's key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync<TKey, TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;

        /// <summary>
        /// Asynchronously adds a range of entities to the repository.
        /// </summary>
        /// <typeparam name="TKey">The type of the entity's key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entities">The entities to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddRangeAsync<TKey, TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;

        /// <summary>
        /// Asynchronously updates an existing entity in the repository.
        /// </summary>
        /// <typeparam name="TKey">The type of the entity's key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync<TKey, TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;

        /// <summary>
        /// Asynchronously deletes an existing entity from the repository.
        /// </summary>
        /// <typeparam name="TKey">The type of the entity's key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync<TKey, TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;
    }

    /// <summary>
    /// Contract for performing CRUD operations on entities of type TEntity.
    /// <para>Using this as a dependency allows you to limit consumers to querying a specific <typeparamref name="TEntity"/>.</para>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        /// <summary>
        /// Asynchronously adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously adds a range of entities to the repository.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously deletes an existing entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Contract for performing CRUD operations at a generic interface level with a particular entity with a particular key.
    /// <para>Using this as a dependency allows you to limit consumers to querying a specific <typeparamref name="TEntity"/>.</para>
    /// </summary>
    /// <typeparam name="TKey">The type of the entity's key.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TKey, TEntity> : IReadOnlyRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        /// <summary>
        /// Asynchronously adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously adds a range of entities to the repository.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously deletes an existing entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
