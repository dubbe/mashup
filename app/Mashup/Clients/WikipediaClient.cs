using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Mashup.DTO;
using Mashup.Factories;

namespace Mashup.Clients
{
    public class WikipediaClient : BaseClient<Wikipedia>, IWikipediaClient
    {
        private IWikidataClient _wikidata;
        public WikipediaClient(HttpClient httpClient, ISerializerFactory serializerFactory, IWikidataClient wikidata) : base(httpClient, serializerFactory)
        {
            _wikidata = wikidata;
        }
        public async Task<Wikipedia> GetAsync(Musicbrainz musicbrainz)
        {
            string title = musicbrainz.WikipediaTitle;
            if (String.IsNullOrEmpty(title) && musicbrainz.WikidataId != null)
            {
                Wikidata wikidata = await _wikidata.GetTitle(musicbrainz.WikidataId);
                if (wikidata != null)
                {
                    title = wikidata.Title;
                }
            }

            if (!String.IsNullOrEmpty(title))
            {
                return await getDescription(title);
            } else {
                throw new Exception();
            }
        }

        private async Task<Wikipedia> getDescription(string title) {
            HttpRequestMessage request = CreateRequest(title);
            return await SendAsync(request);
        }
        
        private static HttpRequestMessage CreateRequest(string title) {
            return new HttpRequestMessage(HttpMethod.Get, string.Format("https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles={0}", HttpUtility.UrlEncode(title)));
        }
    }
}