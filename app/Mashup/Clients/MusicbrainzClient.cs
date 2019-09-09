using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Mashup.DTO;
using Mashup.Serializers;
using Newtonsoft.Json;

namespace Mashup.Clients
{
    public class MusicbrainzClient : BaseClient<Musicbrainz>
    {
        public MusicbrainzClient(HttpClient httpClient, ISerializer<Musicbrainz> deserializer) : base(httpClient, deserializer)
        {
        }

        public async Task<Musicbrainz> GetArtist(string MBID) {
            HttpRequestMessage request = CreateRequest(MBID);
            return await SendAsync(request);
        }

        private static HttpRequestMessage CreateRequest(string MBID) {
            return new HttpRequestMessage(HttpMethod.Get, "http://musicbrainz.org/ws/2/artist/" + MBID + "?&fmt=json&inc=url-rels+release-groups");
        }
    }
}