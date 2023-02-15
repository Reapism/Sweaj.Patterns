namespace Sweaj.Patterns.Data.Entities
{
    /// <summary>
    /// Represents a polymorphic object.
    /// <para>Allows an instance of the object to represent either an owner, creator, delegator, or target.</para>
    /// </summary>
    /// <remarks>
    /// Allows an object to be polymorpic by
    /// specifying the name and key of the reference for the relationship
    /// to a type that implements this interface.
    /// </remarks>
    public interface IReferenceable<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The primary key of the reference.
        /// </summary>
        TKey ReferenceId { get; set; }
        /// <summary>
        /// The name of the reference.
        /// </summary>
        /// <remarks>
        /// The name of the reference that is related in someway
        /// to an instance of the implemented type.
        /// </remarks>
        string ReferenceName { get; set; }
    }
}
