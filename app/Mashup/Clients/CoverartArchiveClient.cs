using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Mashup.DTO;
using Mashup.Factories;


namespace Mashup.Clients
{
    public class CoverartArchiveClient : BaseClient<CoverartArchive>, ICoverartArchiveClient
    {
        
        public CoverartArchiveClient(HttpClient httpClient, ISerializerFactory serializerFactory) : base(httpClient, serializerFactory)
        {
        }

        public async Task<List<CoverartArchive>> GetAsync(Musicbrainz musicbrainz)
        {
            var tasks = new List<Task<CoverartArchive>>();

            foreach (MusicbrainzAlbum musicbrainzAlbum in musicbrainz.Albums)
            {
                tasks.Add(Task.Run(() => getImage(musicbrainzAlbum.Id)));
            }

            return (await Task.WhenAll(tasks)).ToList();
        }

        private async Task<CoverartArchive> getImage(string id) {
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