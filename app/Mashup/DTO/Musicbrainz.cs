using System.Collections.Generic;

namespace Mashup.DTO
{
    public class Musicbrainz
    {

        private readonly string _mbid;

        public string MBID
        {
            get
            {
                return _mbid;
            }
        }

        private readonly string _wikidataId;

        public string WikidataId
        {
            get
            {
                return _wikidataId;
            }
        }

        private readonly string _wikipediaTitle;
        public string WikipediaTitle
        {
            get
            {
                return _wikipediaTitle;
            }
        }

        private readonly IList<MusicbrainzAlbum> _albums;
        public IList<MusicbrainzAlbum> Albums
        {
            get
            {
                return _albums;
            }
        }

        public Musicbrainz(string mbid, string wikidataId, string wikipediaTitle, IList<MusicbrainzAlbum> albums)
        {
            _mbid = mbid;
            _wikidataId = wikidataId;
            _wikipediaTitle = wikipediaTitle;

            _albums = albums;
        }
    }
}