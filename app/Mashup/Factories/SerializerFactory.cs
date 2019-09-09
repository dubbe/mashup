using System;
using Mashup.DTO;
using Mashup.Serializers;

namespace Mashup.Factories
{
    public class SerializerFactory
    {

        public ISerializer<T> Create<T>()
        {
            if (typeof(T) == typeof(Musicbrainz))
            {
                return (ISerializer<T>) new MusicbrainzSerializer();
            }

            if (typeof(T) == typeof(Wikidata))
            {
                return (ISerializer<T>) new WikidataSerializer();
            }

            if (typeof(T) == typeof(Wikipedia))
            {
                return (ISerializer<T>) new WikipediaSerializer();
            }

            if (typeof(T) == typeof(CoverartArchive))
            {
                return (ISerializer<T>) new CoverartArchiveSerializer();
            }

            throw new InvalidOperationException();

        }
    }
}