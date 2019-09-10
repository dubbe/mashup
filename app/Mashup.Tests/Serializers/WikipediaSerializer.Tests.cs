using System.IO;
using Mashup.DTO;
using Mashup.Serializers;
using Newtonsoft.Json;
using Xunit;

namespace Mashup.Tests.Serializers
{
    public class WikipediaSerializerTests
    {

        private string jsonString = "{\"batchcomplete\":\"\",\"warnings\":{\"extracts\":{\"*\":\"HTML may be malformed and/or unbalanced and may omit inline images. Use at your own risk. Known problems are listed at https://www.mediawiki.org/wiki/Extension:TextExtracts#Caveats.\"}},\"query\":{\"normalized\":[{\"from\":\"Nirvana_(band)\",\"to\":\"Nirvana (band)\"}],\"pages\":{\"21231\":{\"pageid\":21231,\"ns\":0,\"title\":\"Nirvana (band)\",\"extract\":\"<p class=\\\"mw-empty-elt\\\">\\n</p>\\n\\n<p class=\\\"mw-empty-elt\\\">\\n\\n</p>\\n<p><b>Nirvana</b> was an American rock band formed in Aberdeen, Washington, in 1987. It was founded by lead singer and guitarist Kurt Cobain and bassist Krist Novoselic.   Nirvana went through a succession of drummers, the longest-lasting and best-known being Dave Grohl, who joined in 1990. Though the band dissolved in 1994 after the death of Cobain, their music maintains a popular following and continues to influence modern rock and roll culture.\\n</p><p>In the late 1980s, Nirvana established itself as part of the Seattle grunge scene, releasing its first album, <i>Bleach</i>, for the independent record label Sub Pop in 1989. They developed a sound that relied on dynamic contrasts, often between quiet verses and loud, heavy choruses. After signing to major label DGC Records, in 1991, Nirvana found unexpected mainstream worldwide success with \\\"Smells Like Teen Spirit\\\", the first single from their landmark second album <i>Nevermind</i> (1991), which sold over 30 million copies worldwide. Nirvana's sudden success popularized alternative rock, and Cobain found himself described as the \\\"spokesman of a generation\\\" and Nirvana the \\\"flagship band\\\" of Generation X.</p><p>Following extensive tours and the 1992 EPs <i>Incesticide</i> and <i>Hormoaning</i>, Nirvana released their third studio album, <i>In Utero</i> (1993), to critical acclaim and further chart success. Its abrasive, less mainstream sound challenged the band's audience, and has since sold over 15 million copies worldwide. Nirvana disbanded following the death of Cobain in 1994. Many various posthumous releases have been issued, overseen by Novoselic, Grohl, and Cobain's widow Courtney Love. The posthumous release <i>MTV Unplugged in New York</i> (1994) won the Grammy Award for Best Alternative Music Album in 1996.\\n</p><p>Nirvana received various awards, including an American Music Award, Brit Award, Grammy Award, seven MTV Video Music Awards and two NME Awards. They have sold over 25 million records in the United States and over 75 million records worldwide, making them one of the best-selling bands of all time. Nirvana has also been ranked as one of the greatest music artists of all time, with <i>Rolling Stone</i> placing them at number 27 on their list of the 100 Greatest Artists of All Time in 2004, and at number 30 on their updated list in 2011. Nirvana was inducted into the Rock and Roll Hall of Fame in their first year of eligibility in 2014.\\n</p>\\n\\n\\n\"}}}}";
        

        [Fact]
        public void JsonStringShouldReturnDescription() {
            

            JsonTextReader reader = new JsonTextReader(new StringReader(jsonString));

            WikipediaSerializer wikipediaSerializer = new WikipediaSerializer();
            Wikipedia wikipedia = wikipediaSerializer.Deserialize(reader, "https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles=Nirvana_(band)");

            // TODO expand this test
            Assert.NotNull(wikipedia.Description);
 
        }
    }
}