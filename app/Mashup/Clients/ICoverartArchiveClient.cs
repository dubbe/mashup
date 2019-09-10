using System.Collections.Generic;
using System.Threading.Tasks;
using Mashup.DTO;

namespace Mashup.Clients
{
    public interface ICoverartArchiveClient
    {
         Task<List<CoverartArchive>> GetAsync(Musicbrainz musicbrainz);
    }
}