namespace Mashup.DTO
{
    public class Wikidata
    {
        
        private readonly string _title;
        
        public string Title {get {
            return _title;
        }}

        public Wikidata(string title) {
            _title = title;
        }
    }
}