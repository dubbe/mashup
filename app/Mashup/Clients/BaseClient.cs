using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Mashup.Factories;
using Mashup.Serializers;
using Newtonsoft.Json;

namespace Mashup.Clients
{
    public class BaseClient<T>
    {

        private readonly HttpClient _httpClient;
        private readonly ISerializer<T> _deserilzer;
        public BaseClient(HttpClient httpClient, ISerializerFactory serializerFactory) {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _deserilzer = serializerFactory.Create<T>() ?? throw new ArgumentNullException(nameof(httpClient));
        }

        protected async Task<T> SendAsync(HttpRequestMessage request) {
 
            // TODO looking up proxy servers?
            string requestUri = request.RequestUri.ToString();

            request.Headers.Add("User-Agent", "mashup/0.0.1 (thomas.dahlberg@cygni.se)");
            if (request.Content == null)
            {
                request.Content = new StringContent("");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            HttpResponseMessage result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            result.EnsureSuccessStatusCode(); // Will throw exception if not successful

            using (var streamReader = new StreamReader(await result.Content.ReadAsStreamAsync()))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return _deserilzer.Deserialize(jsonTextReader, requestUri);
            }
        }

    }
}