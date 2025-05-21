namespace SetmoreSharp
{
    public partial class SetmoreClient : ApiClient
    {
        public async Task<IEnumerable<Staff>> GetStaffAsync()
        {
            var allStaff = new List<Staff>();
            string cursor = null;

            do
            {
                // build your request, appending cursor if you have one
                var request = new Request
                {
                    EndPoint = cursor is null
                        ? "staffs"
                        : $"staffs?cursor={Uri.EscapeDataString(cursor)}"
                };

                var response = await GetAsync<ApiResponse<StaffDto>>(request);
                var dto = response.Data;

                if (dto?.Staff != null && dto.Staff.Any())
                {
                    allStaff.AddRange(dto.Staff);
                }

                cursor = dto?.Cursor;
            }
            while (cursor.HasValue());

            return allStaff;
        }
    }
}