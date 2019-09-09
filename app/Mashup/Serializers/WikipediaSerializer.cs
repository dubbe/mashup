using System.Linq;
using Mashup.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mashup.Serializers
{
    public class WikipediaSerializer : ISerializer<Wikipedia>
    {
        public Wikipedia Deserialize(JsonTextReader jsonTextReader)
        {
            JObject o = (JObject)JToken.ReadFrom(jsonTextReader);
            return new Wikipedia(o.SelectToken("$.query..extract").ToString());
        }
    }
}