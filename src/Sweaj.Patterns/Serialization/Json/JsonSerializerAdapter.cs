using System.Text.Json;

namespace Sweaj.Patterns.Serialization.Json
{
    public sealed class JsonSerializerAdapter : IJsonSerializer
    {
        public T? Deserialize<T>(string serializedValue)
        {
            var instance = JsonSerializer.Deserialize<T>(serializedValue);
            return instance;
        }     

        public async ValueTask<T?> DeserializeAsync<T>(Stream value)
        {
            return await JsonSerializer.DeserializeAsync<T>(value, JsonSerializerOptionsProvider.Web);
        }

        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, JsonSerializerOptionsProvider.Web);
        }

        public async ValueTask<Stream> SerializeAsync<T>(T value)
        {
            Stream stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, value, JsonSerializerOptionsProvider.Web);
            return stream;
        }
    }
}
