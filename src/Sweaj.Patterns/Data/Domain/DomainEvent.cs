using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Data.Domain
{
    [Trackable]
    public abstract class DomainEvent
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
    }
}