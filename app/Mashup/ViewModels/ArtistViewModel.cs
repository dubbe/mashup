using System.Collections.Generic;

namespace Mashup.ViewModels
{
    public class ArtistViewModel
    {
        public string MBID {get;set;}
        public string Description { get; set; }
        public List<AlbumViewModel> Albums { get; set; }
    }
}