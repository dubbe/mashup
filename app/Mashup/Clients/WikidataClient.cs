using System.Net.Http;
using System.Threading.Tasks;
using Mashup.DTO;
using Mashup.Factories;

namespace Mashup.Clients
{
    public class WikidataClient : BaseClient<Wikidata>, IWikidataClient
    {
        public WikidataClient(HttpClient httpClient, ISerializerFactory serializerFactory) : base(httpClient, serializerFactory)
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