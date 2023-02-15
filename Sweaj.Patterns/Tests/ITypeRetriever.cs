using System.Reflection;

namespace Sweaj.Patterns.Tests
{
    public interface ITypeRetriever
{
    Type[] GetTypesWithAttribute<TAttribute>(Assembly assembly, Func<TAttribute, Type, bool> filter = null)
        where TAttribute : Attribute;
}
}