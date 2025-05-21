namespace SetmoreSharp
{
    public partial class SetmoreClient : ApiClient
    {
        public static int DefaultPageSize = 100;

        private double _apiLimit = 0;

        public SetmoreClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

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