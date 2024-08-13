using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Serialization.Json
{
    [Trackable]
    public interface IJsonSerializer
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string json);
        ValueTask<Stream> SerializeAsync<TValue>(TValue value);
        ValueTask<TValue> DeserializeAsync<TValue>(Stream value);
    }
}
