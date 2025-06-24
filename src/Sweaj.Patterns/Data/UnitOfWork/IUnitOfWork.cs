using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Entities;
using Sweaj.Patterns.Data.Repositories;

namespace Sweaj.Patterns.Data.UnitOfWork
{
    /// <summary>
    /// Generic interface for unit of work pattern.
    /// </summary>
    [Trackable]
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits all changes made in the unit of work asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous commit operation.</returns>
        Task<int> CommitAsync();
        /// <summary>
        /// Returns a repository tracked by this instance.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : Entity, IAggregateRoot;
    }
}
