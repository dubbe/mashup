using Mashup.Clients;

namespace Mashup.Repositories
{
    public interface IMashupRepository
    {
        MusicbrainzClient Musicbrainz {get;}
        WikidataClient Wikidata {get;}
        WikipediaClient Wikipedia {get;}
        CoverartArchiveClient CoverartArchive {get;}
    }
}