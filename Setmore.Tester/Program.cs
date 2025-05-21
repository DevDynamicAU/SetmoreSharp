using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SetmoreSharp;
using SetmoreSharp.Web;

namespace Setmore.Tester
{
    internal partial class Program
    {
        private static IServiceProvider _serviceProvider;

        private static string ProdServiceUrl = "https://developer.setmore.com/api";
        private static string ApiVersion = "v1";

        private static SetmoreClient _client;

        private static async Task Main(string[] args)
        {
            var targetUrl = ProdServiceUrl;
            var apiVersion = ApiVersion;

            var host = Host.CreateDefaultBuilder(args)
                        .ConfigureServices((ctx, services) =>
                        {
                            Console.WriteLine($"Environment = {ctx.HostingEnvironment.EnvironmentName}");

                            // read from IConfiguration
                            var cfg = ctx.Configuration;
                            var apiKey = cfg.GetValue<string>("ApiSettings:ApiKey") ?? "";

                            RegisterServices(services, targetUrl, apiVersion, apiKey);
                        })
                        .Build();

            _serviceProvider = host.Services;

            var skipEnvCheck = targetUrl == ProdServiceUrl;

            _client = _serviceProvider.GetService<SetmoreClient>();

            WriteLine($"Setmore API Test - {targetUrl}");

            var doTestsResp = 'y';
            if (!skipEnvCheck)
            {
                WriteLine("Continue (Y/y)");
                doTestsResp = Console.ReadKey().KeyChar;
            }

            if (doTestsResp.ToString().ToLower() == "y")
            {
                //await ServiceTestsAsync();
                //await StaffTestsAsync();
                await TimeSlotTestsAsync();

                WriteLine("End");
            }
            else
            {
                WriteLine("");
                WriteLine("Testing aborted");
            }
        }

        public static bool CheckResponse<T>(T response, string prefix, string fieldName, string operation)
        {
            //if (response == null)
            //{
            //    WriteLine($"{prefix}: Response is null");
            //    return false;
            //}

            //var isArray = response is Array;
            //var isBool = response is bool;
            //var isApiResponse = response is ApiResponse;

            //if (isArray)
            //{
            //    var arrayResponse = response as Array;

            //    if (arrayResponse.Length > 0)
            //    {
            //        WriteLine($"{prefix}: {arrayResponse.Length} records {operation}");
            //        return true;
            //    }
            //    else
            //    {
            //        WriteLine($"{prefix}: No records found");
            //        return false;
            //    }
            //}

            //if (response is System.Collections.IEnumerable enumerable && response is not string)
            //{
            //    var count = enumerable.Cast<object>().Count();
            //    if (count > 0)
            //    {
            //        WriteLine($"{prefix}: {count} records {operation}");
            //        return true;
            //    }
            //    else
            //    {
            //        WriteLine($"{prefix}: No records found");
            //        return false;
            //    }
            //}

            //if (isBool)
            //{
            //    var boolResp = response as bool?;
            //    var responseResult = boolResp.HasValue && boolResp.Value ? "successfully" : "failed";

            //    WriteLine($"{prefix}: {operation} {responseResult}");

            //    return boolResp ?? false;
            //}

            //if (isApiResponse)
            //{
            //    var apiResponse = response as ApiResponse;
            //    var responseResult = apiResponse != null && !apiResponse.HasErrors ? "successfully" : "failed";

            //    WriteLine($"{prefix}: {operation} {responseResult}");

            //    if (apiResponse != null && apiResponse.HasErrors)
            //    {
            //        foreach (var error in apiResponse.Errors)
            //        {
            //            WriteLine($"{prefix}: {error.Message}");
            //        }
            //    }

            //    return apiResponse != default(ApiResponse) ? true : false;
            //}

            //// Some other sort of response
            //if (!fieldName.HasValue())
            //{
            //    WriteLine($"{prefix}: FieldName can not be null or empty - response check skipped");
            //    return false;
            //}

            //var type = response.GetType();
            //var property = type.GetProperty(fieldName);

            //if (property == null)
            //{
            //    WriteLine($"{prefix}: Field '{fieldName}' not found on type '{type.Name}'");
            //    return false;
            //}

            //var fieldValue = property.GetValue(response);

            //if (fieldValue != null)
            //{
            //    WriteLine($"{prefix}: {type.Name} {fieldValue} was {operation}");
            //    return true;
            //}
            //else
            //{
            //    WriteLine($"{prefix}: Field '{fieldName}' is null");
            //}

            return false;
        }

        private static void RegisterServices(IServiceCollection services, string serviceUrl, string apiVersion, string refreshToken)
        {
            WriteLine("RegisterServices");

            services.AddSetmoreClients(serviceUrl, apiVersion, refreshToken);
        }

        public static void WriteLine(string info)
        {
            Console.WriteLine(info);
            System.Diagnostics.Trace.WriteLine(info);
        }
    }
}