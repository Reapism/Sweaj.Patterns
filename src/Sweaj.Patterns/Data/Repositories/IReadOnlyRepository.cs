using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Entities;

namespace Sweaj.Patterns.Data.Repositories
{
    [Trackable]
    public interface IReadOnlyRepository
    {
        IQueryable Query<TEntity>();
    }

    [Trackable]
    public interface IReadOnlyRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        IQueryable<TEntity> Query();
    }

    [Trackable]
    public interface IReadOnlyRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        IQueryable<TEntity> Query();
    }
}
