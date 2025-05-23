﻿using SetmoreSharp;

namespace Setmore.Tester.Tests.Service
{
    public static partial class ServiceTests
    {
        public static async Task GetServicesAsync(SetmoreClient client)
        {
            Program.WriteLine("BEGIN GetAllServicesAsync");

            try
            {
                var resp = await client.GetServicesAsync();

                Program.WriteLine($"{resp?.Count() ?? 0} records found");
                //Program.CheckResponse(resp, "GetAllServicesAsync", "Id", "found");
            }
            catch (Exception ex)
            {
                Program.WriteLine($"Error: {ex}");
            }

            Program.WriteLine("END GetAllServicesAsync");
        }
    }
}