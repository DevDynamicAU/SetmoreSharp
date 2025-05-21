using SetmoreSharp.Models;

namespace SetmoreSharp
{
    public partial class SetmoreClient : ApiClient
    {
        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            var request = new Request();

            request.EndPoint = "services";

            var response = await GetAsync<ApiResponse<ServicesDto>>(request);

            return response.Data.Services;
        }

        public async Task<IEnumerable<Service>> GetServicesByCategoryAsync(string categoryId)
        {
            var request = new Request();

            request.EndPoint = $"services/categories/{categoryId}";

            var response = await GetAsync<ApiResponse<ServicesDto>>(request);

            return response.Data.Services;
        }

        public async Task<IEnumerable<ServiceCategory>> GetServiceCategoriesAsync()
        {
            var request = new Request();

            request.EndPoint = "services/categories";

            var response = await GetAsync<ApiResponse<ServiceCategoriesDto>>(request);

            return response.Data.ServiceCategories;
        }
    }
}