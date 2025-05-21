using Setmore.Tester.Tests.Staff;

namespace Setmore.Tester
{
    internal partial class Program
    {
        public static async Task StaffTestsAsync()
        {
            Program.WriteLine("BEGIN StaffTests");

            await StaffTests.GetStaffAsync(_client);

            WriteLine("END StaffTests");
        }
    }
}