using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Serialization
{
    [Trackable]
    public interface ISerializer<TSerializedType, TDeserializedType>
    {
        TSerializedType Serialize(TDeserializedType value);
        TDeserializedType Deserialize(TSerializedType serializedType);
    }
}
