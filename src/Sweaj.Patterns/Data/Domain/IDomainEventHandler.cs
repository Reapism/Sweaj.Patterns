using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Data.Domain
{
    [Trackable]
    public interface IDomainEventHandler<in T>
        where T : DomainEvent
    {
        Task HandleAsync(T domainEvent);
    }
}