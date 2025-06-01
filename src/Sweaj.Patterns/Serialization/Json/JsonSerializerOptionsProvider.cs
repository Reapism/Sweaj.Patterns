using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sweaj.Patterns.Serialization.Json
{
    /// <summary>
    /// Provides reusable and pre-configured <see cref="JsonSerializerOptions"/> instances for JSON serialization.
    /// </summary>
    public static class JsonSerializerOptionsProvider
    {
        private static readonly JsonSerializerOptions DefaultInstance = new(JsonSerializerDefaults.Web)
        {
            AllowTrailingCommas = false,
            NumberHandling = JsonNumberHandling.Strict,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private static readonly JsonSerializerOptions WebWithOutputFormatting = new(DefaultInstance)
        {
            WriteIndented = true,
        };

        /// <summary>
        /// Gets a preconfigured <see cref="JsonSerializerOptions"/> instance using the <see cref="JsonSerializerDefaults.Web"/> defaults,
        /// with camel case naming, strict number handling, and cycle reference handling.
        /// </summary>
        public static JsonSerializerOptions Web => DefaultInstance;

        /// <summary>
        /// Gets a version of <see cref="Web"/> with <see cref="JsonSerializerOptions.WriteIndented"/> enabled.
        /// Useful for producing human-readable JSON, such as for logging.
        /// </summary>
        public static JsonSerializerOptions WebFormatted => WebWithOutputFormatting;

        /// <summary>
        /// Adds a list of <see cref="JsonConverter"/> instances to the specified <see cref="JsonSerializerOptions"/>.
        /// Existing converters are cleared before adding the new ones.
        /// </summary>
        /// <param name="jsonSerializerOptions">The <see cref="JsonSerializerOptions"/> instance to modify.</param>
        /// <param name="jsonConverters">A collection of <see cref="JsonConverter"/> instances to add.</param>
        /// <returns>The updated <see cref="JsonSerializerOptions"/> instance.</returns>
        public static JsonSerializerOptions WithConverters(this JsonSerializerOptions jsonSerializerOptions, IEnumerable<JsonConverter> jsonConverters)
        {
            jsonSerializerOptions.Converters.Clear();
            foreach (var jsonConverter in jsonConverters)
            {
                jsonSerializerOptions.Converters.Add(jsonConverter);
            }

            return jsonSerializerOptions;
        }

        /// <summary>
        /// Searches the specified assemblies for types that inherit from <see cref="JsonConverter"/>
        /// and returns instances of those converters.
        /// </summary>
        /// <param name="assembliesToSearch">An array of <see cref="Assembly"/> objects to search for converters.</param>
        /// <returns>
        /// A list of instantiated <see cref="JsonConverter"/> objects found within the specified assemblies.
        /// </returns>
        public static IList<JsonConverter> GetJsonConverters(Assembly[] assembliesToSearch)
        {
            var converters = new List<JsonConverter>();

            foreach (var assembly in assembliesToSearch)
            {
                var types = assembly.GetTypes();

                for (int i = 0; i < types.Length; i++)
                {
                    var type = types[i];
                    if (typeof(JsonConverter).IsAssignableFrom(type))
                    {
                        var converter = (JsonConverter?)Activator.CreateInstance(type);
                        if (converter is not null)
                            converters.Add(converter);
                    }
                }
            }

            return converters;
        }
    }
}
