using System.Collections.Generic;
using System.Threading.Tasks;
using Mashup.Clients;
using Mashup.Controllers;
using Mashup.DTO;
using Mashup.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Mashup.Tests.Controllers
{
    public class MashupControllerTests
    {
        [Fact]
        public async Task TestController()
        {

            var musicbrainz = new Musicbrainz(
                "5b11f4ce-a62d-471e-81fc-a69a8278c7da",
                "wikidata-id",
                "wikipedia-title",
                new List<MusicbrainzAlbum>() {
                    new MusicbrainzAlbum("album-id", "album-title")
                }
            );

            var mockMusicbrainzClient = new Mock<IMusicbrainzClient>(MockBehavior.Strict);
            mockMusicbrainzClient.Setup(x => x.GetAsync("5b11f4ce-a62d-471e-81fc-a69a8278c7da")).Returns(Task.FromResult(
                musicbrainz
            ));

            var mockWikipediaClient = new Mock<IWikipediaClient>(MockBehavior.Strict);
            mockWikipediaClient.Setup(x => x.GetAsync(musicbrainz)).Returns(Task.FromResult(
                new Wikipedia(
                   "testar bara description"
                )
            ));

            var mockCoverartClient = new Mock<ICoverartArchiveClient>(MockBehavior.Strict);
            mockCoverartClient.Setup(x => x.GetAsync(musicbrainz)).Returns(Task.FromResult(
                new List<CoverartArchive>() {
                    new CoverartArchive(
                    "5b11f4ce-a62d-471e-81fc-a69a8278c7da",
                    "coverart-image"
                )}
            ));

            MashupController controller = new MashupController(mockMusicbrainzClient.Object, mockWikipediaClient.Object, mockCoverartClient.Object);

            ActionResult<ArtistViewModel> result = await controller.GetAsync("5b11f4ce-a62d-471e-81fc-a69a8278c7da");
            Assert.Equal("5b11f4ce-a62d-471e-81fc-a69a8278c7da", result.Value.MBID);
        }
    }
}