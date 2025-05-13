using Duende.IdentityModel.Client;
using SetmoreSharp.Models;
using System.Text.Json;

namespace SetmoreSharp
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly ClientCredentials _clientCredentials;
        private readonly IHttpClientFactory _httpClientFactory;

        private SetmoreTokenResponse _token;

        private DateTime _expiryTime;

        private JsonSerializerOptions _jsonSerializerOptions;

        public AuthenticationDelegatingHandler(ClientCredentials clientCredentials, IHttpClientFactory httpClientFactory)
        {
            _clientCredentials = clientCredentials;
            _httpClientFactory = httpClientFactory;

            _jsonSerializerOptions = Helpers.JsonSerializerOptions;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await RefreshTokenAsync();

            if (_token != null)
            {
                request.SetBearerToken(_token.AccessToken);
            }

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }

        private async Task RefreshTokenAsync()
        {
            if (_token != null && !_token.IsError && _expiryTime > DateTime.UtcNow)
            {
                return;
            }

            var client = _httpClientFactory.CreateClient();
            var loginData = CreateLoginData();

            var authUrl = new Uri($"https://developer.setmore.com/api/v1/o/oauth2/token?refreshToken={_clientCredentials.RefreshToken}");
            var response = await client.GetAsync(authUrl);
            var data = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode && data != null)
            {
                var resp = JsonSerializer.Deserialize<ApiResponse<SetmoreTokenResponse>>(data, _jsonSerializerOptions) ?? new ApiResponse<SetmoreTokenResponse>() { Data = new SetmoreTokenResponse() { Error = "Error decoding response" } };

                _token = resp.Data;
            }
            else
            {
                throw new Exception(data);
            }

            if (_token.IsError)
            {
                _expiryTime = DateTime.UtcNow;
                throw new Exception(_token.Error);
            }

            _expiryTime = DateTime.UtcNow.AddSeconds(_token.ExpiresIn);
        }

        private HttpContent CreateLoginData()
        {
            var data = new Dictionary<string, string>()
            {
                { "refreshToken", _clientCredentials.RefreshToken },
            };

            return new FormUrlEncodedContent(data);
        }
    }
}