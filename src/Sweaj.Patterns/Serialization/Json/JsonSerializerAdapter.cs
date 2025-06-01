using System.Text.Json;

namespace Sweaj.Patterns.Serialization.Json
{
    /// <summary>
    /// An adapter that implements <see cref="IJsonSerializer"/> using <see cref="System.Text.Json.JsonSerializer"/>.
    /// </summary>
    public sealed class JsonSerializerAdapter : IJsonSerializer
    {
        /// <inheritdoc/>
        public T? Deserialize<T>(string serializedValue)
        {
            var instance = JsonSerializer.Deserialize<T>(serializedValue);
            return instance;
        }

        /// <inheritdoc/>
        public async ValueTask<T?> DeserializeAsync<T>(Stream value)
        {
            return await JsonSerializer.DeserializeAsync<T>(
                value,
                JsonSerializerOptionsProvider.Web
            );
        }

        /// <inheritdoc/>
        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, JsonSerializerOptionsProvider.Web);
        }

        /// <inheritdoc/>
        public async ValueTask<Stream> SerializeAsync<T>(T value)
        {
            Stream stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, value, JsonSerializerOptionsProvider.Web);
            stream.Position = 0; // Ensure the stream is rewinded before returning
            return stream;
        }
    }
}
