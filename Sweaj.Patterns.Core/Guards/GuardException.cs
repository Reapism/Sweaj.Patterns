namespace Sweaj.Patterns.Guards
{
    public sealed class GuardException : Exception
    {
        private const string DefaultGuardExceptionMessage = "A default exception thrown by a IGuardClause extension method which can be tracked.";
        public GuardException([NotNull] Exception innerException)
            : base(DefaultGuardExceptionMessage, innerException)
        { }
    }
}
