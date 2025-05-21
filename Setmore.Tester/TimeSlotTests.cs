using Setmore.Tester.Tests.TimeSlot;

namespace Setmore.Tester
{
    internal partial class Program
    {
        public static async Task TimeSlotTestsAsync()
        {
            Program.WriteLine("BEGIN TimeSlotTests");

            await TimeSlotTests.GetAvailableTimeSlotsAsync(_client);

            WriteLine("END TimeSlotTests");
        }
    }
}