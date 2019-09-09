namespace Mashup.DTO
{
    public class MusicbrainzAlbum
    {
        private readonly string _id;

        public string ID
        {
            get
            {
                return _id;
            }
        }
        private readonly string _title;

        public string Title
        {
            get
            {
                return _title;
            }
        }

        public MusicbrainzAlbum() {
            
        }

        public MusicbrainzAlbum(string id, string title) {
            _id = id;
            _title = title;
        }
    }
}