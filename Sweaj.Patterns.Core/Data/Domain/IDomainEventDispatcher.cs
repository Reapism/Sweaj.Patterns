namespace Sweaj.Patterns.Data.Domain
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(DomainEvent domainEvent);
    }
}