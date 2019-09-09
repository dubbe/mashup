using Mashup.DTO;
using Mashup.Factories;
using Mashup.Serializers;
using Xunit;

namespace Mashup.Tests.Factories
{
    public class FactoriesTest
    {
        [Fact]
        public void SerializerFactoryWithTypeArtistShouldReturnMusicbrainzSerializer() {
            var type = new SerializerFactory().Create<Musicbrainz>();
            Assert.IsType<MusicbrainzSerializer>(type);
        }

        [Fact]
        public void SerializerFactoryWithTypeWikidataShouldReturnWikidataSerializer() {
            var type = new SerializerFactory().Create<Wikidata>();
            Assert.IsType<WikidataSerializer>(type);
        }

        [Fact]
        public void SerializerFactoryWithTypeWikidataShouldReturnWikipediaSerializer() {
            var type = new SerializerFactory().Create<Wikipedia>();
            Assert.IsType<WikipediaSerializer>(type);
        }
    }
}