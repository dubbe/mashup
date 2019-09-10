using System.Threading.Tasks;
using Mashup.DTO;

namespace Mashup.Clients
{
    public interface IMusicbrainzClient
    {
        Task<Musicbrainz> GetAsync(string MBID) ;
    }
}