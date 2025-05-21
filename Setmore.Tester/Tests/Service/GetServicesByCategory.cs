using SetmoreSharp;

namespace Setmore.Tester.Tests.Service
{
    public static partial class ServiceTests
    {
        public static async Task GetServicesByCategoryAsync(SetmoreClient client)
        {
            Program.WriteLine("BEGIN GetServicesByCategoryAsync");

            try
            {
                var categories = await client.GetServiceCategoriesAsync();
                if (categories == null || !categories.Any())
                {
                    Program.WriteLine("No service categories found.");
                    return;
                }

                var categoryId = categories.First().Id;

                var resp = await client.GetServicesByCategoryAsync(categoryId);

                Program.WriteLine($"{resp?.Count() ?? 0} records found");
                //Program.CheckResponse(resp, "GetAllServicesAsync", "Id", "found");
            }
            catch (Exception ex)
            {
                Program.WriteLine($"Error: {ex}");
            }

            Program.WriteLine("END GetServicesByCategoryAsync");
        }
    }
}