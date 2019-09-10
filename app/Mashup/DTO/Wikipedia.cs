namespace Mashup.DTO
{
    public class Wikipedia
    {
        
        private readonly string _description;
        
        public string Description {get {
            return _description;
        }}

        public Wikipedia(string description) {
            _description = description;
        }
    }
}