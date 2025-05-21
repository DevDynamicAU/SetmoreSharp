using System.Text;
using System.Text.Json;

namespace SetmoreSharp
{
    public class ApiClient
    {
        public const string ClientName = "SetmoreClient";

        private readonly IHttpClientFactory _httpClientFactory;
        private JsonSerializerOptions _jsonSerializerOptions;

        public ApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _jsonSerializerOptions = Helpers.JsonSerializerOptions;
        }

        protected async Task<T> GetAsync<T>(Request request) where T : class
        {
            var client = _httpClientFactory.CreateClient(ClientName);
            var message = new HttpRequestMessage(HttpMethod.Get, request.GetUrl());

            //var response = await client.GetAsync(request.GetUrl(), HttpCompletionOption.ResponseHeadersRead);
            var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);

            var data = await response.Content.ReadAsStringAsync();

            // Rate limit to one request per second
            Thread.Sleep(1000);

            if (response.IsSuccessStatusCode)
            {
                var result = Helpers.DeserialiseResponse<T>(data, _jsonSerializerOptions); //JsonSerializer.Deserialize<T>(data, _jsonSerializerOptions);
                return result;
            }
            else
            {
                throw new Exception($"{response.StatusCode} - {response.ReasonPhrase}, accessing {response.RequestMessage?.RequestUri}");
            }
        }

        protected async Task<T> PostAsync<T, F>(Request request, ApiBody<F> content) where T : class
        {
            var client = _httpClientFactory.CreateClient(ClientName);
            var url = request.GetUrl();
            var httpContent = CreateHttpContent(content.Content);

            var response = await client.PostAsync(url, httpContent);
            var data = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(data, _jsonSerializerOptions);

            if (!response.IsSuccessStatusCode && apiResponse != null)
            {
                throw new Exception($"Error in request - isSuccessCode: {response.IsSuccessStatusCode}");
            }

            return apiResponse.Data;
        }

        protected async Task<T> PostAsync<T>(Request request, object content)
        {
            var client = _httpClientFactory.CreateClient(ClientName);

            var body = CreateHttpContent(content);

            var message = new HttpRequestMessage(HttpMethod.Post, request.GetUrl());
            message.Content = body;

            //var response = await client.PostAsync(request.GetUrl(), body);
            var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);

            var data = await response.Content.ReadAsStringAsync();

            // Detect if T is ApiResponse<Something>
            var t = typeof(T);
            var isApiResponse = t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ApiPostResponse<>);

            // If it's NOT an ApiResponse<TPayload>, enforce HTTP success
            if (!isApiResponse && !response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed {(int)response.StatusCode} {response.StatusCode}: {data}");
            }

            // Always attempt to de-serialise — if it's ApiResponse<>, you'll get the full payload (even on errors);
            // otherwise you only reach here on 2xx.
            var result = JsonSerializer.Deserialize<T>(data, _jsonSerializerOptions) ?? throw new JsonException($"Could not de-serialise JSON to {t.Name}");

            return result;
        }

        protected async Task PostAsync(Request request, object content)
        {
            var client = _httpClientFactory.CreateClient(ClientName);

            await client.PostAsync(request.GetUrl(), CreateHttpContent(content));
        }

        protected async Task<T> PatchAsync<T>(Request request, object content)
        {
            var client = _httpClientFactory.CreateClient(ClientName);

            var message = new HttpRequestMessage(HttpMethod.Patch, request.GetUrl());
            message.Content = CreateHttpContent(content);

            //var response = await client.PatchAsync(request.GetUrl(), CreateHttpContent(content));
            var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);

            var data = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<T>(data, _jsonSerializerOptions);
            }
            else
            {
                throw new Exception(data);
            }
        }

        protected async Task PatchAsync(Request request, object content)
        {
            var client = _httpClientFactory.CreateClient(ClientName);
            var message = new HttpRequestMessage(HttpMethod.Patch, request.GetUrl());
            message.Content = CreateHttpContent(content);

            //await client.PatchAsync(request.GetUrl(), CreateHttpContent(content));
            var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);
        }

        protected async Task DeleteAsync(Request request)
        {
            var client = _httpClientFactory.CreateClient(ClientName);
            var message = new HttpRequestMessage(HttpMethod.Delete, request.GetUrl());

            //await client.DeleteAsync(request.GetUrl());
            await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonSerializer.Serialize(content, _jsonSerializerOptions);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}