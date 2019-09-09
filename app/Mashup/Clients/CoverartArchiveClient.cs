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
    public class CoverartArchiveClient : BaseClient<CoverartArchive>
    {
        public CoverartArchiveClient(HttpClient httpClient, ISerializer<CoverartArchive> deserializer) : base(httpClient, deserializer)
        {
        }

        public async Task<CoverartArchive> GetImage(string id) {
            HttpRequestMessage request = CreateRequest(id);
            return await SendAsync(request);
        }
        
        
        private static HttpRequestMessage CreateRequest(string id) {
            return new HttpRequestMessage(HttpMethod.Get, string.Format("http://coverartarchive.org/release-group/{0}", id));
        }
    }
}