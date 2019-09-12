using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mashup.ViewModels;
using Mashup.DTO;
using System.Linq;
using Mashup.Clients;

namespace Mashup.Controllers
{
    [Route("api/mashup")]
    [ApiController]
    public class MashupController : ControllerBase
    {
        private IMusicbrainzClient _musicbrainz;
        private IWikipediaClient _wikipedia;
        private ICoverartArchiveClient _coverartArchive;

        public MashupController(IMusicbrainzClient musicbrainz, IWikipediaClient wikipedia, ICoverartArchiveClient coverartArchive)
        {
            _musicbrainz = musicbrainz;
            _wikipedia = wikipedia;
            _coverartArchive = coverartArchive;
        }

        // GET api/mashup/:mbid
        [HttpGet]
        [Route("{mbid}")]
        public async Task<ActionResult<ArtistViewModel>> GetAsync(string mbid)
        {

            Musicbrainz musicbrainz = await _musicbrainz.GetAsync(mbid);

            if (musicbrainz.MBID == null)
            {
                return new NotFoundResult();
            }

            var wikipediaTask = _wikipedia.GetAsync(musicbrainz);
            var coverartTask = _coverartArchive.GetAsync(musicbrainz);

            await Task.WhenAll(wikipediaTask, coverartTask);

            return buildAlbumViewModel(musicbrainz, await wikipediaTask, await coverartTask);

        }

        private ArtistViewModel buildAlbumViewModel(Musicbrainz musicbrainz, Wikipedia wikipedia, IList<CoverartArchive> coverarts) {
            ArtistViewModel artistViewModel = new ArtistViewModel();
            artistViewModel.Albums = new List<AlbumViewModel>();

            artistViewModel.MBID = musicbrainz.MBID;
                artistViewModel.Description = wikipedia.Description;

                foreach(var album in musicbrainz.Albums) {
                    artistViewModel.Albums.Add(new AlbumViewModel() {
                        Id = album.Id,
                        Title = album.Title,
                        Image = coverarts.FirstOrDefault(x => x.Id == album.Id)?.Image
                    });
                }

                return artistViewModel;
        }

        

        

    }
}
