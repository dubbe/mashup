using System.Net.Http;
using Mashup.Clients;
using Mashup.DTO;
using Mashup.Factories;

namespace Mashup.Repositories
{
    public class MashupRepository : IMashupRepository
    {
        //private HttpClient _httpClient;
        
        private readonly MusicbrainzClient _musicbrainzClient;
        private readonly WikidataClient _wikidataClient;
        private readonly WikipediaClient _wikipediaClient;
        private readonly CoverartArchiveClient _coverartArchiveClient;
        public MusicbrainzClient Musicbrainz {
             get { return _musicbrainzClient;}
        }
        public WikidataClient Wikidata {
            get { return _wikidataClient;}
        }
        public WikipediaClient Wikipedia {
            get { return _wikipediaClient;}
        }
        public CoverartArchiveClient CoverartArchive {
            get { return _coverartArchiveClient;}
        }


        public MashupRepository(HttpClient httpClient, ISerializerFactory serializerFactory) {
             //_httpClient = httpClient;
             //HttpClient httpClient = clientFactory.CreateClient();

             _musicbrainzClient = new MusicbrainzClient(httpClient, serializerFactory.Create<Musicbrainz>());
             _wikidataClient = new WikidataClient(httpClient, serializerFactory.Create<Wikidata>());
             _wikipediaClient = new WikipediaClient(httpClient, serializerFactory.Create<Wikipedia>());
             _coverartArchiveClient = new CoverartArchiveClient(httpClient, serializerFactory.Create<CoverartArchive>());
         }
    }
}