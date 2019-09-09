using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Mashup.DTO;
using Mashup.Serializers;
using Newtonsoft.Json;

namespace Mashup.Clients
{
    public class WikidataClient : BaseClient<Wikidata>
    {
        public WikidataClient(HttpClient httpClient, ISerializer<Wikidata> deserializer) : base(httpClient, deserializer)
        {
        }

        public async Task<Wikidata> GetTitle(string id) {
            HttpRequestMessage request = CreateRequest(id);
            return await SendAsync(request);
        }
        
        
        private static HttpRequestMessage CreateRequest(string id) {
            return new HttpRequestMessage(HttpMethod.Get, "https://www.wikidata.org/w/api.php?action=wbgetentities&ids=" + id + "&format=json&props=sitelinks");
        }
    }
}