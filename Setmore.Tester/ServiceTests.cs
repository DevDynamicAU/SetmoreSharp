using Setmore.Tester.Tests.Service;

namespace Setmore.Tester
{
    internal partial class Program
    {
        public static async Task ServiceTestsAsync()
        {
            Program.WriteLine("BEGIN ServiceTests");

            //await ServiceTests.GetServicesAsync(_client);
            //await ServiceTests.GetServiceCategoriesAsync(_client);
            await ServiceTests.GetServicesByCategoryAsync(_client);

            WriteLine("END ServiceTests");
        }
    }
}