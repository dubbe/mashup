using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Mashup.DTO;
using Mashup.Serializers;
using Newtonsoft.Json;

namespace Mashup.Clients
{
    public class WikipediaClient : BaseClient<Wikipedia>
    {
        public WikipediaClient(HttpClient httpClient, ISerializer<Wikipedia> deserializer) : base(httpClient, deserializer)
        {
        }

        public async Task<Wikipedia> GetDescription(string title) {
            HttpRequestMessage request = CreateRequest(title);
            return await SendAsync(request);
        }
        
        
        private static HttpRequestMessage CreateRequest(string title) {
            return new HttpRequestMessage(HttpMethod.Get, string.Format("https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles={0}", HttpUtility.UrlEncode(title)));
        }
    }
}