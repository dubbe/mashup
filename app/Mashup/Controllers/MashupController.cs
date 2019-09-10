using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mashup.ViewModels;
using Mashup.Factories;
using Mashup.Repositories;
using Mashup.DTO;
using System.Linq;

namespace Mashup.Controllers
{
    [Route("api/mashup")]
    [ApiController]
    public class MashupController : ControllerBase
    {
        private IMashupRepository _repository;

        public MashupController(IMashupRepository repository)
        {
            //serializerFactory = new SerializerFactory();
            _repository = repository;
        }

        // GET api/mashup/:mbid
        [HttpGet]
        [Route("{mbid}")]
        public async Task<ActionResult<ArtistViewModel>> GetAsync(string mbid)
        {
            try
            {
                ArtistViewModel artistViewModel = new ArtistViewModel();
                artistViewModel.Albums = new List<AlbumViewModel>();

                // Get from musicbrainz first
                Musicbrainz musicbrainz = await _repository.Musicbrainz.Get(mbid);

                if (musicbrainz.MBID == null)
                {
                    return new NotFoundResult();
                }

                var wikipediaTask = getDescription(musicbrainz);
                var coverartTask = getCoverarts(musicbrainz);

                await Task.WhenAll(wikipediaTask, coverartTask);

                var wikipedia = await wikipediaTask;
                var coverarts = await coverartTask;

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
            catch (Exception)
            {
                // TODO better error handling
                return new NotFoundResult();
            }
        }

        private async Task<Wikipedia> getDescription(Musicbrainz musicbrainz)
        {
            string title = musicbrainz.WikipediaTitle;
            if (String.IsNullOrEmpty(title) && musicbrainz.WikidataId != null)
            {
                Wikidata wikidata = await _repository.Wikidata.GetTitle(musicbrainz.WikidataId);
                if (wikidata != null)
                {
                    title = wikidata.Title;
                }
            }

            if (!String.IsNullOrEmpty(title))
            {
                return await _repository.Wikipedia.GetDescription(title);
            } else {
                throw new Exception();
            }
        }

        private async Task<List<CoverartArchive>> getCoverarts(Musicbrainz musicbrainz)
        {
            var tasks = new List<Task<CoverartArchive>>();

            foreach (MusicbrainzAlbum musicbrainzAlbum in musicbrainz.Albums)
            {
                tasks.Add(Task.Run(() => getCoverart(musicbrainzAlbum)));
            }

            return (await Task.WhenAll(tasks)).ToList();
        }

        private async Task<CoverartArchive> getCoverart(MusicbrainzAlbum musicbrainzAlbum)
        {
            return await _repository.CoverartArchive.GetImage(musicbrainzAlbum.Id);
        }

    }
}
