namespace Mashup.DTO
{
    public class CoverartArchive
    {
        private readonly string _image;
        
        public string Image {get {
            return _image;
        }}

        public CoverartArchive() {
            
        }
        public CoverartArchive(string image) {
            _image = image;
        }
    }
}