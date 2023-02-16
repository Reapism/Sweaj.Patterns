namespace Sweaj.Patterns.Data.Domain
{
    public interface IDomainEventProvider
    {
        List<DomainEvent> Events { get; }
    }
}