using System.Text.Json;

namespace Sweaj.Patterns.Serialization.Json
{
    public sealed class JsonSerializerAdapter : IJsonSerializer
    {
        public T Deserialize<T>(string serializedValue)
        {
            var instance = JsonSerializer.Deserialize<T>(serializedValue);

#pragma warning disable CS8603 // Possible null reference return.
            return Guard.Against.Null(instance);
#pragma warning restore CS8603 // Possible null reference return.
        }     

        public ValueTask<T> DeserializeAsync<T>(Stream value)
        {

            return JsonSerializer.DeserializeAsync<T>(value, JsonSerializerOptionsProvider.Web);
        }

        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, JsonSerializerOptionsProvider.Web);
        }

        public ValueTask<Stream> SerializeAsync<T>(T value)
        {
            Stream stream = new MemoryStream();
            JsonSerializer.SerializeAsync(stream, value, JsonSerializerOptionsProvider.Web);
            return ValueTask.FromResult(stream);
        }
    }
}
