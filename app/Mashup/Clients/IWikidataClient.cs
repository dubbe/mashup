using System.Threading.Tasks;
using Mashup.DTO;

namespace Mashup.Clients
{
    public interface IWikidataClient
    {
        Task<Wikidata> GetTitle(string id);
    }
}