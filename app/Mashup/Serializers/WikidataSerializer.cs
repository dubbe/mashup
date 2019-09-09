using System.Linq;
using Mashup.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mashup.Serializers
{
    public class WikidataSerializer : ISerializer<Wikidata>
    {
        public Wikidata Deserialize(JsonTextReader jsonTextReader)
        {
            JObject o = (JObject)JToken.ReadFrom(jsonTextReader);
            return new Wikidata(o.SelectToken("$..enwiki.title").ToString());
        }
    }
}