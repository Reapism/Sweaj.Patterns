using System.Text.Json;

namespace Sweaj.Patterns.Serialization.Json
{
    public sealed class JsonSerializerAdapter<T> : IJsonSerializer<T>
    {
        public T Deserialize(string serializedValue)
        {
            var instance = JsonSerializer.Deserialize<T>(serializedValue);

#pragma warning disable CS8603 // Possible null reference return.
            return Guard.Against.Null(instance);
#pragma warning restore CS8603 // Possible null reference return.
        }     

        public ValueTask<T> DeserializeAsync(Stream value)
        {

            return JsonSerializer.DeserializeAsync<T>(value, JsonSerializerOptionsProvider.Web);
        }

        public string Serialize(T value)
        {
            return JsonSerializer.Serialize(value, JsonSerializerOptionsProvider.Web);
        }

        public ValueTask<Stream> SerializeAsync(T value)
        {
            sizeof(T)
            Stream stream = new MemoryStream(sizeof(value));
            JsonSerializer.SerializeAsync(stream, value, JsonSerializerOptionsProvider.Web);
            return ValueTask.FromResult(stream);
        }
    }
}
