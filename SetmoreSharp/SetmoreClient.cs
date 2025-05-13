using SetmoreSharp.Models;

namespace SetmoreSharp
{
    public partial class SetmoreClient : ApiClient
    {
        public static int DefaultPageSize = 100;

        private double _apiLimit = 0;

        //private string _gqlApiVersion = "2025-04"; //"2024-04";

        //public SetmoreClient(string baseEndpoint, string refreshToken, string apiVersion = "v1")
        //{
        //}
        public SetmoreClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            var request = new Request();

            request.EndPoint = "bookingapi/services";

            var response = await GetAsync<ApiResponse<List<Service>>>(request);

            return response.Data;
        }

        //protected int? CheckPageSize(int? pageSize)
        //{
        //    if (pageSize == null) return pageSize;

        //    return pageSize > ShopifyClient.DefaultPageSize ? ShopifyClient.DefaultPageSize : pageSize;
        //}

        //protected int ApiLimit()
        //{
        //    return (int)(_apiLimit * 1000d);
        //}

        public async Task RetryAsync(Func<Task> job)
        {
            var retries = 0;
            var flag = false;

            while (!flag)
            {
                try
                {
                    await job();

                    flag = true;
                }
                catch
                {
                    retries++;

                    Thread.Sleep(1000);

                    if (retries > 5)
                    {
                        throw;
                    }
                }
            }
        }
    }
}