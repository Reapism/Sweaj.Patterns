using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Data.Domain
{
    [Trackable]
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(DomainEvent domainEvent);
    }
}