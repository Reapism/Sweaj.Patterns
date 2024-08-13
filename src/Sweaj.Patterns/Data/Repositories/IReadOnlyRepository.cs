using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Entities;
using System.Linq.Expressions;

namespace Sweaj.Patterns.Data.Repositories
{
    [Trackable]
    public interface IReadOnlyRepository
    {
        Task<TEntity> GetByIdAsync<TKey, TEntity>(TKey id, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;

        Task<TEntity> GetWhenAsync<TKey, TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;

        Task<List<TEntity>> ListAsync<TKey, TEntity>(CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;

        Task<List<TEntity>> ListAsync<TKey, TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;
    }

    [Trackable]
    public interface IReadOnlyRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<TEntity> GetWhenAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<List<TEntity>> ListAsync(CancellationToken cancellationToken);
        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }

    [Trackable]
    public interface IReadOnlyRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken);
        Task<TEntity> GetWhenAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<List<TEntity>> ListAsync(CancellationToken cancellationToken);
        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }
}
