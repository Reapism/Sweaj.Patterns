using Sweaj.Patterns.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweaj.Patterns.Serialization
{
    [Trackable]
    public interface ISerializer<TSerializedType, TDeserializedType>
    {
        TSerializedType Serialize(TDeserializedType value);
        TDeserializedType Deserialize(TSerializedType serializedType);
    }
}
