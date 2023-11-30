namespace Sweaj.Patterns.Data.Entities
{
    /// <summary>
    /// Represents an aggregate root in the domain model.
    /// </summary>
    /// <remarks>
    /// This is a marker interface used to indicate that an entity is the root of an aggregate.
    /// An aggregate is a cluster of domain objects that can be treated as a single unit.
    /// The aggregate root is the entity that holds a reference to each of the other entities in the aggregate.
    /// 
    /// This interface is typically used to restrict the repositories to only operate on aggregate roots, ensuring 
    /// data consistency rules are preserved and also preventing independent persistence of internal entities.
    /// 
    /// An entity that has this interface should have its own repository and should be the only object that 
    /// client code loads directly.
    /// </remarks>
    public interface IAggregateRoot
    { }
}
