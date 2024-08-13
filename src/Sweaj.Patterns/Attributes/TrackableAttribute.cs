namespace Sweaj.Patterns.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TrackableAttribute : Attribute
    {
    }
}
