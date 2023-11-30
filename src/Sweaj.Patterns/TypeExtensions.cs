using System.Reflection;

namespace Sweaj.Patterns
{

    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>([NotNull] this Assembly assembly, [CanBeNull] Func<TAttribute, Type, bool> filter = null)
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

        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>([NotNull] this IEnumerable<Assembly> assemblies, [CanBeNull] Func<TAttribute, Type, bool> filter = null)
            where TAttribute : Attribute
        {
            var types = new List<Type>();

            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly?.GetTypes()
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
               .ToArray() ?? Array.Empty<Type>());
            }

            return types;
        }
    }
}