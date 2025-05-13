using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace SetmoreSharp.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSetmoreClients(this IServiceCollection services, IConfiguration configuration, string configSection = "Setmore")
        {
            var siteConfig = configuration.GetSection(configSection);

            if (siteConfig != null)
            {
                // Should be everything up till the api version. e.g. https://developer.setmore.com/api
                var baseEndpoint = siteConfig["EndPoint"];
                var apiVersion = siteConfig["ApiVersion"];
                var refreshToken = siteConfig["RefreshToken"];

                if (!string.IsNullOrEmpty(baseEndpoint) && !string.IsNullOrEmpty(refreshToken))
                {
                    services.AddSetmoreClients(baseEndpoint, apiVersion, refreshToken);
                }
                else
                {
                    // todo throw an error?
                }
            }
            else
            {
                throw new InvalidOperationException($"Unable to find config section '{configSection}'");
            }

            return services;
        }

        public static IServiceCollection AddSetmoreClients(this IServiceCollection services, string baseEndpoint, string apiVersion, string refreshToken)
        {
            if (baseEndpoint is null || apiVersion is null || refreshToken is null)
            {
                return services;
            }

            var clientCreds = new ClientCredentials
            {
                RefreshToken = refreshToken
            };

            services.AddSingleton(clientCreds);
            services.AddTransient<AuthenticationDelegatingHandler>();

            services.AddHttpClient(ApiClient.ClientName, client =>
            {
                var baseUrl = $"{baseEndpoint}/{apiVersion}";
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                // explicitly opt into TLS 1.2 and 1.3
                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13 // not using Tls or Tls1
            })
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            services.AddTransient<SetmoreClient>();

            return services;
        }
    }
}