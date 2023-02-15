using Sweaj.Patterns.Data.Entities;

namespace Sweaj.Patterns.Data.Repositories
{
    /// <summary>
    /// Contract for CRUDing at generic method level.
    /// <para>Using this as a dependency allows you to reuse
    /// this dependency and querying multiple T's.</para>
    /// </summary>
    public interface IRepository : IReadOnlyRepository
    {
        Task AddAsync<TKey, TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;
        Task AddRangeAsync<TKey, TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;
        Task UpdateAsync<TKey, TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;
        Task DeleteAsync<TKey, TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;
    }

    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Contract for CRUDing at generic interface level.
    /// <para>Using this as a dependency allows you to limit
    /// consumers to querying a <typeparamref name="TEntity"/>.</para>
    /// </summary>
    public interface IRepository<TKey, TEntity> : IReadOnlyRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
