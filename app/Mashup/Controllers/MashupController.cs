using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mashup.ViewModels;
using Mashup.Factories;
using Mashup.Repositories;
using Mashup.DTO;

namespace Mashup.Controllers
{
    [Route("api/mashup")]
    [ApiController]
    public class MashupController : ControllerBase
    {
        private SerializerFactory serializerFactory;
        private MashupRepository mashupRepository;
        private ArtistViewModel artistViewModel;

        public MashupController()
        {
            serializerFactory = new SerializerFactory();
            mashupRepository = new MashupRepository(new System.Net.Http.HttpClient(), serializerFactory);
        }

        // GET api/mashup/:mbid
        [HttpGet]
        [Route("{mbid}")]
        public async Task<ActionResult<ArtistViewModel>> GetAsync(string mbid)
        {
            try
            {
                artistViewModel = new ArtistViewModel();
                artistViewModel.Albums = new List<AlbumViewModel>();

                // Get from musicbrainz first
                Musicbrainz musicbrainz = await mashupRepository.Musicbrainz.Get(mbid);

                if (musicbrainz.MBID == null)
                {
                    return new NotFoundResult();
                }

                var tasks = new List<Task>()
                {
                    getDescription(musicbrainz),
                    getCoverarts(musicbrainz)
                };

                await Task.WhenAll(tasks);

                return artistViewModel;
            }
            catch (Exception)
            {
                // TODO better error handling
                return new NotFoundResult();
            }
        }

        private async Task getDescription(Musicbrainz musicbrainz)
        {
            string title = musicbrainz.WikipediaTitle;
            if (String.IsNullOrEmpty(title) && musicbrainz.WikidataId != null)
            {
                Wikidata wikidata = await mashupRepository.Wikidata.GetTitle(musicbrainz.WikidataId);
                if (wikidata != null)
                {
                    title = wikidata.Title;
                }
            }

            if (!String.IsNullOrEmpty(title))
            {
                Wikipedia wikipedia = await mashupRepository.Wikipedia.GetDescription(title);
                artistViewModel.MBID = musicbrainz.MBID;
                artistViewModel.Description = wikipedia.Description;
            }
        }

        private async Task getCoverarts(Musicbrainz musicbrainz)
        {
            var tasks = new List<Task>();

            foreach (MusicbrainzAlbum musicbrainzAlbum in musicbrainz.Albums)
            {
                tasks.Add(getCoverart(musicbrainzAlbum));
            }

            await Task.WhenAll(tasks);
        }

        private async Task getCoverart(MusicbrainzAlbum musicbrainzAlbum)
        {
            CoverartArchive coverartArchive = await mashupRepository.CoverartArchive.GetImage(musicbrainzAlbum.ID);
            artistViewModel.Albums.Add(new AlbumViewModel()
            {
                Id = musicbrainzAlbum.ID,
                Title = musicbrainzAlbum.Title,
                Image = coverartArchive.Image
            });
        }


    }
}
