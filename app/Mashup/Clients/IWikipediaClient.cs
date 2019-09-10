using System.Threading.Tasks;
using Mashup.DTO;

namespace Mashup.Clients
{
    public interface IWikipediaClient
    {
        Task<Wikipedia> GetAsync(Musicbrainz musicbrainz);
    }
}