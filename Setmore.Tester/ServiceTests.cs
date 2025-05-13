using Setmore.Tester.Tests.Service;

namespace Setmore.Tester
{
    internal partial class Program
    {
        public static async Task ServiceTestsAsync()
        {
            Program.WriteLine("BEGIN PositionTests");

            await ServiceTests.GetAllServicesAsync(_client);

            WriteLine("END PositionTests");
        }
    }
}