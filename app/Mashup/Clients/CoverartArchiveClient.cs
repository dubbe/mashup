using System.Net.Http;
using System.Threading.Tasks;
using Mashup.DTO;
using Mashup.Serializers;


namespace Mashup.Clients
{
    public class CoverartArchiveClient : BaseClient<CoverartArchive>
    {
        public CoverartArchiveClient(HttpClient httpClient, ISerializer<CoverartArchive> deserializer) : base(httpClient, deserializer)
        {
        }

        public async Task<CoverartArchive> GetImage(string id) {
            HttpRequestMessage request = CreateRequest(id);
            try{
                return await SendAsync(request);
            } catch (HttpRequestException) {

                // Could not find coverart, return empty-coverart
                return new CoverartArchive();
            }
        }
        
        
        private static HttpRequestMessage CreateRequest(string id) {
            return new HttpRequestMessage(HttpMethod.Get, string.Format("http://coverartarchive.org/release-group/{0}", id));
        }
    }
}