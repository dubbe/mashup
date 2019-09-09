using Newtonsoft.Json;

namespace Mashup.Serializers
{
    public interface ISerializer<T>
    {
        T Deserialize(JsonTextReader jsonTextReader);
    }
}