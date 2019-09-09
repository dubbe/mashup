using System.Linq;
using Mashup.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mashup.Serializers
{
    public class MusicbrainzSerializer : ISerializer<Musicbrainz>
    {
        public Musicbrainz Deserialize(JsonTextReader jsonTextReader)
        {
            JObject o = (JObject)JToken.ReadFrom(jsonTextReader);
            return new Musicbrainz(
                o.SelectToken("$.id")?.ToString(),
                o.SelectToken("$.relations[?(@.type == 'wikidata')].url.resource")?.ToString().Split('/').Last(),
                o.SelectToken("$.relations[?(@.type == 'wikipedia')].url.resource")?.ToString().Split('/').Last(),
                o.SelectTokens("$..release-groups[*]").Select(x => new MusicbrainzAlbum(
                    x.SelectToken("id")?.ToString(),
                    x.SelectToken("title")?.ToString())).ToList()
            );
        }

    }

    
}