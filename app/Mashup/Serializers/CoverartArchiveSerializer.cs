using System;
using System.Linq;
using Mashup.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mashup.Serializers
{
    public class CoverartArchiveSerializer : ISerializer<CoverartArchive>
    {
        public CoverartArchive Deserialize(JsonTextReader jsonTextReader)
        {
            try
            {
                JObject o = (JObject)JToken.ReadFrom(jsonTextReader);

                return new CoverartArchive(o.SelectTokens("$.images[?(@.front)].image").FirstOrDefault().ToString());
            }
            catch (Exception)
            {
                return new CoverartArchive("");
            }
        }
    }
}