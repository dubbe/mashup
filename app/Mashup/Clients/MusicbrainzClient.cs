using System.Net.Http;
using System.Threading.Tasks;
using Mashup.DTO;
using Mashup.Factories;

namespace Mashup.Clients
{
    public class MusicbrainzClient : BaseClient<Musicbrainz>, IMusicbrainzClient
    {
        public MusicbrainzClient(HttpClient httpClient, ISerializerFactory serializerFactory) : base(httpClient, serializerFactory)
        {
        }

        public async Task<Musicbrainz> GetAsync(string MBID) {
            HttpRequestMessage request = CreateRequest(MBID);
            return await SendAsync(request);
        }

        private static HttpRequestMessage CreateRequest(string MBID) {
            return new HttpRequestMessage(HttpMethod.Get, "http://musicbrainz.org/ws/2/artist/" + MBID + "?&fmt=json&inc=url-rels+release-groups");
        }
    }
}