namespace Mashup.DTO
{
    public class CoverartArchive
    {
        private readonly string _id;
        private readonly string _image;
        
        public string Id { get {
            return _id;
        }}
        public string Image {get {
            return _image;
        }}

        public CoverartArchive() {
            
        }
        public CoverartArchive(string id, string image) {
            _id = id;
            _image = image;
        }
    }
}