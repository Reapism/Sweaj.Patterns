using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sweaj.Patterns.Json
{
    public static class JsonSerializerOptionsProvider
    {
        private static readonly JsonSerializerOptions DefaultInstance = new(JsonSerializerDefaults.Web)
        {
            AllowTrailingCommas = false,
            NumberHandling = JsonNumberHandling.Strict,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
        public static JsonSerializerOptions Web => DefaultInstance;

        private static readonly JsonSerializerOptions DefaultInstanceWithConverters = new(JsonSerializerDefaults.Web)
        {
            AllowTrailingCommas = false,
            NumberHandling = JsonNumberHandling.Strict,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public static JsonSerializerOptions WithConverters(JsonSerializerOptions jsonSerializerOptions, IEnumerable<JsonConverter> jsonConverters)
        {
            var options = new JsonSerializerOptions(jsonSerializerOptions);

            foreach (var jsonConverter in jsonConverters)
            {
                options.Converters.Add(jsonConverter);
            }

            return options;
        }

        public static IList<JsonConverter> GetJsonConverters(Assembly[] assembliesToSearch)
        {
            var converters = new List<JsonConverter>();

            foreach (var assembly in assembliesToSearch)
            {
                // Get all types in the assembly
                var types = assembly.GetTypes();

                for (int i = 0; i < types.Length; i++)
                {
                    var type = types[i];
                    if (typeof(JsonConverter).IsAssignableFrom(type))
                    {
                        // Create an instance of the JsonConverter type and add it to the list
                        var converter = (JsonConverter)Activator.CreateInstance(type);
                        if (converter is not null)
                            converters.Add(converter);
                    }
                }
            }

            return converters;
        }
    }
}
