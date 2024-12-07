using Ardalis.GuardClauses;
using Sweaj.Patterns.Attributes;
using System.Text.Json;

namespace Sweaj.Patterns.Data.Attributes
{
    /// <summary>
    /// Marks a property
    /// </summary>
    [Trackable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class PolymorphicAttribute : Attribute
    {
        public string TypePropertyName { get; set; }
        public string DataPropertyName { get; }
        public PolymorphicAttribute([ValidatedNotNull]string typePropertyName, [ValidatedNotNull]string dataPropertyName)
        {
            TypePropertyName = Guard.Against.NullOrWhiteSpace(typePropertyName);
            DataPropertyName = Guard.Against.NullOrWhiteSpace(dataPropertyName);
        }
    }

    public static class PolymorphicSerializer
    {
        private const string Delimiter = "::";

        public static string SerializePolymorphically<T>(T instance)
        {
            var typeName = typeof(T).AssemblyQualifiedName;
            var serializedData = JsonSerializer.Serialize(instance);
            return $"{typeName}{Delimiter}{serializedData}";
        }
    }

}
