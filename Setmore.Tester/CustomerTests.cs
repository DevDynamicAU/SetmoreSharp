using Setmore.Tester.Tests.Customer;

namespace Setmore.Tester
{
    internal partial class Program
    {
        public static async Task CustomerTestsAsync()
        {
            Program.WriteLine("BEGIN CustomerTests");

            //await CustomerTests.GetCustomerAsync(_client);
            await CustomerTests.CreateCustomerAsync(_client);

            WriteLine("END CustomerTests");
        }
    }
}