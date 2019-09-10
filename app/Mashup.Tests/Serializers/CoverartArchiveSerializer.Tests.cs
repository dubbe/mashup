using System.IO;
using Mashup.DTO;
using Mashup.Serializers;
using Newtonsoft.Json;
using Xunit;

namespace Mashup.Tests.Serializers
{
    public class CoverarrArchiveSerializerTests
    {

        private string jsonString = "{\"images\":[{\"types\":[\"Front\"],\"front\":true,\"back\":false,\"edit\":20473306,\"image\":\"http://coverartarchive.org/release/a146429a-cedc-3ab0-9e41-1aaf5f6cdc2d/3012495605.jpg\",\"comment\":\"\",\"approved\":true,\"thumbnails\":{\"large\":\"http://coverartarchive.org/release/a146429a-cedc-3ab0-9e41-1aaf5f6cdc2d/3012495605-500.jpg\",\"small\":\"http://coverartarchive.org/release/a146429a-cedc-3ab0-9e41-1aaf5f6cdc2d/3012495605-250.jpg\"},\"id\":\"3012495605\"}],\"release\":\"http://musicbrainz.org/release/a146429a-cedc-3ab0-9e41-1aaf5f6cdc2d\"}";
        

        [Fact]
        public void JsonStringShouldReturnImage() {
            

            JsonTextReader reader = new JsonTextReader(new StringReader(jsonString));

            CoverartArchiveSerializer coverartArchiveSerializer = new CoverartArchiveSerializer();
            CoverartArchive coverart = coverartArchiveSerializer.Deserialize(reader, "http://coverartarchive.org/release-group/1b022e01-4da6-387b-8658-8678046e4cef");

            Assert.Equal("1b022e01-4da6-387b-8658-8678046e4cef", coverart.Id);
            Assert.Equal("http://coverartarchive.org/release/a146429a-cedc-3ab0-9e41-1aaf5f6cdc2d/3012495605.jpg", coverart.Image);
 
        }

        [Fact]
        public void EmptyStringShouldReturnEmptyString() {
            JsonTextReader reader = new JsonTextReader(new StringReader(""));

            CoverartArchiveSerializer coverartArchiveSerializer = new CoverartArchiveSerializer();
            CoverartArchive coverart = coverartArchiveSerializer.Deserialize(reader, "http://coverartarchive.org/release-group/1b022e01-4da6-387b-8658-8678046e4cef");

            Assert.Empty(coverart.Image);
        }

        [Fact]
        public void JsonStringWithMultipleImagesShouldReturnFrontImage() {
            string jsonStringMultipleImages = "{\"release\":\"https://musicbrainz.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b\",\"images\":[{\"back\":false,\"types\":[\"Front\"],\"image\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443439796.jpg\",\"front\":true,\"approved\":true,\"thumbnails\":{\"small\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443439796-250.jpg\",\"large\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443439796-500.jpg\"},\"comment\":\"\",\"edit\":42329559,\"id\":15443439796},{\"id\":15443441080,\"edit\":42329560,\"comment\":\"\",\"thumbnails\":{\"large\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443441080-500.jpg\",\"small\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443441080-250.jpg\"},\"approved\":true,\"front\":false,\"image\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443441080.jpg\",\"types\":[\"Back\",\"Spine\"],\"back\":true},{\"comment\":\"\",\"thumbnails\":{\"small\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443441405-250.jpg\",\"large\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443441405-500.jpg\"},\"id\":15443441405,\"edit\":42329561,\"types\":[\"Booklet\"],\"back\":false,\"image\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443441405.jpg\",\"approved\":true,\"front\":false},{\"id\":15443441701,\"edit\":42329563,\"comment\":\"\",\"thumbnails\":{\"small\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443441701-250.jpg\",\"large\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443441701-500.jpg\"},\"image\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443441701.jpg\",\"approved\":true,\"front\":false,\"types\":[\"Medium\"],\"back\":false},{\"id\":15443442271,\"edit\":42329564,\"comment\":\"\",\"thumbnails\":{\"large\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443442271-500.jpg\",\"small\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443442271-250.jpg\"},\"image\":\"http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443442271.jpg\",\"approved\":true,\"front\":false,\"back\":false,\"types\":[\"Medium\"]}]}";
        
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonStringMultipleImages));

            CoverartArchiveSerializer coverartArchiveSerializer = new CoverartArchiveSerializer();
            CoverartArchive coverart = coverartArchiveSerializer.Deserialize(reader, "http://coverartarchive.org/release-group/1b022e01-4da6-387b-8658-8678046e4cef");

            Assert.Equal("http://coverartarchive.org/release/7b43c443-d8c1-4122-a248-6f87984ab30b/15443439796.jpg", coverart.Image);
        }
    }
}