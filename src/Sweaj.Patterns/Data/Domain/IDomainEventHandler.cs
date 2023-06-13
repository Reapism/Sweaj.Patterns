namespace Sweaj.Patterns.Data.Domain
{
    public interface IDomainEventHandler<in T>
        where T : DomainEvent
    {
        Task HandleAsync(T domainEvent);
    }
}