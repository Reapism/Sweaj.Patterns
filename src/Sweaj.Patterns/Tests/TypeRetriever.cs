using System.Reflection;

namespace Sweaj.Patterns.Tests
{

    public sealed class TypeRetriever : ITypeRetriever
    {
        public Type[] GetTypesWithAttribute<TAttribute>([NotNull] Assembly assembly, [CanBeNull] Func<TAttribute, Type, bool> filter = null)
            where TAttribute : Attribute
        {
            return assembly?.GetTypes()
               .Where(type => type.IsPublic)
               .Where(type =>
               {
                   if (!Attribute.IsDefined(type, typeof(TAttribute)))
                   {
                       return false;
                   }

                   TAttribute attribute = type.GetCustomAttribute<TAttribute>();
                   return filter is null || filter(attribute, type);
               })
               .ToArray() ?? Array.Empty<Type>();
        }
    }
}