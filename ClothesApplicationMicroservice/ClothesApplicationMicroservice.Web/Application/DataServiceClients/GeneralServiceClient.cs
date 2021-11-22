using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Application.DataServiceClients
{
    public class GeneralServiceClient : IGeneralServiceClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly JsonSerializerOptions defaultOptions;

        public GeneralServiceClient(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            this.defaultOptions =  new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
        }
        public async Task<T> GetData<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept", "application/json");

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();

            T result = await JsonSerializer.DeserializeAsync<T>(responseStream, defaultOptions);
            return result;
        }

        public async Task<T> GetDataWithAuthorization<T>(string uri, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", token);
            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();

            T result = await JsonSerializer.DeserializeAsync<T>(responseStream, defaultOptions);
            return result;
        }

        public async Task<HttpResponseMessage> GetHttpResponseMessage(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept", "application/json");
            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return response;
        }

        public async Task<T> PostData<T>(string uri, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add("Accept", "application/json");
            string json = JsonSerializer.Serialize(content);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            using var responseStream = await response.Content.ReadAsStreamAsync();

            T result = await JsonSerializer.DeserializeAsync<T>(responseStream, defaultOptions);

            return result;
        }
    }
}
