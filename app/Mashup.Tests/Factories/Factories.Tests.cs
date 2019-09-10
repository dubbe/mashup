using System;
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

        [Fact]
        public void SerializerFactoryWithTypeCoverartArchiveShouldReturnCoverartSerializer() {
            var type = new SerializerFactory().Create<CoverartArchive>();
            Assert.IsType<CoverartArchiveSerializer>(type);
        }

        [Fact]
        public void SerializerFactoryWithFaultyCoverartArchiveShouldThrowException() {

            Assert.Throws<InvalidOperationException>(() => new SerializerFactory().Create<object>());
        }
    }
}