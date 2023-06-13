namespace Sweaj.Patterns.Data
{
    /// <summary>
    /// Represents a dual-polymorphic object.
    /// </summary>
    /// <remarks>
    /// Allows an object to be polymorpic by
    /// specifying the name and key of both the source and 
    /// target references.
    /// </remarks>
    /// <example>
    /// <code>
    ///       <para><b>UserNotification</b>: A type that implements IDualReferencable</para>
    ///       <para>Target: <b>User</b> who receives the notification.</para>
    ///       <para>Source: <b>User</b> who created the notification.</para>
    /// </code>
    /// </example>
    public interface IDualReferenceable<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The primary key of the source reference.
        /// </summary>
        TKey SourceReferenceId { get; set; }

        /// <summary>
        /// The name of the source reference.
        /// </summary>
        /// <remarks>
        /// The name of the target reference that is related
        /// to an instance of the implemented type by being a target.
        /// </remarks>
        string SourceReferenceName { get; set; }

        /// <summary>
        /// The primary key of the target reference.
        /// </summary>
        TKey TargetReferenceId { get; set; }

        /// <summary>
        /// The name of the target reference.
        /// </summary>
        /// <remarks>
        /// The name of the target reference that is related
        /// to an instance of the implemented type by being a target.
        /// </remarks>
        string TargetReferenceName { get; set; }
    }
}
