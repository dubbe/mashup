using System;
using System.Linq;
using Mashup.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mashup.Serializers
{
    public class CoverartArchiveSerializer : ISerializer<CoverartArchive>
    {
        public CoverartArchive Deserialize(JsonTextReader jsonTextReader, string requestUri)
        {
            try
            {
                // Get id from uri
                string id = requestUri.Split('/').Last();

                JObject o = (JObject)JToken.ReadFrom(jsonTextReader);

                return new CoverartArchive(id, o.SelectTokens("$.images[?(@.front)].image").FirstOrDefault().ToString());
            }
            catch (Exception)
            {
                // TODO Exception
                return new CoverartArchive("", "");
            }
        }
    }
}