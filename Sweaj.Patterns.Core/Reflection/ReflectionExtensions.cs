using System.Linq.Expressions;
using System.Reflection;

namespace Sweaj.Patterns.Reflection
{
    public static class ReflectionExtensions
    {
        public static FieldInfo[] GetFieldsBy<T>(BindingFlags bindingAttrs, Func<FieldInfo, bool> filter)
        {
            return typeof(T).GetFields(bindingAttrs).Where(filter).ToArray();
        }

        public static PropertyInfo[] GetPropertiesBy<T>(BindingFlags bindingAttrs, Func<PropertyInfo, bool> filter)
        {
            return typeof(T).GetProperties(bindingAttrs).Where(filter).ToArray();
        }

        public static MemberInfo[] GetMembersBy<T>(BindingFlags bindingAttrs, Func<MemberInfo, bool> filter)
        {
            return typeof(T).GetMembers(bindingAttrs).Where(filter).ToArray();
        }
    }


}
