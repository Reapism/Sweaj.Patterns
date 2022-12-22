using Sweaj.Patterns.Data.Entities;
using Sweaj.Patterns.Data.Services;

namespace Sweaj.Patterns.Data.UnitOfWork
{
    public interface IUnitOfWork : IRepositoryRetriever
    {
        Task CommitAsync();
        Task Rollback();
    }

    public interface IUnitOfWork<TEntity> : IRepositoryRetriever<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        Task CommitAsync();
        Task Rollback();
    }

    public interface IUnitOfWork<TKey, TEntity> : IRepositoryRetriever<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        Task CommitAsync();
        Task Rollback();
    }
}
