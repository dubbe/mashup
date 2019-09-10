using Mashup.Serializers;

namespace Mashup.Factories
{
    public interface ISerializerFactory
    {
         ISerializer<T> Create<T>();
    }
}