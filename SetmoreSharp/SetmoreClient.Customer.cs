using SetmoreSharp.Models;

namespace SetmoreSharp
{
    public partial class SetmoreClient : ApiClient
    {
        public async Task<IEnumerable<Customer>> GetCustomerAsync(string firstName, string lastName = null, string emailId = null, string phone = null)
        {
            var request = new Request();
            request.EndPoint = "customer";

            if (!firstName.HasValue())
            {
                throw new ArgumentNullException(nameof(firstName), "First name is required.");
            }

            request.AddParameter("firstname", firstName);

            if (emailId.HasValue())
            {
                request.AddParameter("email", emailId);
            }

            if (phone.HasValue())
            {
                request.AddParameter("phone", phone);
            }

            var response = await GetAsync<ApiResponse<CustomerDto>>(request);

            var result = new List<Customer>();

            if (response != null && response.Data.Customers != null)
            {
                result = response.Data.Customers;

                if (lastName.HasValue())
                {
                    result = result.Where(c => c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            return result;
        }

        public async Task<Customer> CreateCustomerAsync(string firstName,
                                                        string lastName,
                                                        string email,
                                                        string countryCode = null,
                                                        string mobile = null,
                                                        string workPhone = null,
                                                        string homePhone = null,
                                                        string address = null,
                                                        string city = null,
                                                        string state = null,
                                                        string postCode = null,
                                                        string comment = null,
                                                        CustomerAdditionalFields additionalFields = null)
        {
            var arguments = new CreateCustomerArguments()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                CountryCode = countryCode,
                Mobile = mobile,
                WorkPhone = workPhone,
                HomePhone = homePhone,
                Address = address,
                City = city,
                State = state,
                PostCode = postCode,
                Comment = comment,
                AdditionalFields = additionalFields
            };

            var result = await CreateCustomerAsync(arguments);

            return result;
        }

        public async Task<Customer> CreateCustomerAsync(CreateCustomerArguments customerArguments)
        {
            var request = new Request();

            request.EndPoint = "customer/create";

            var body = new ApiBody<CreateCustomerArguments>(customerArguments);

            var resp = await PostAsync<CreateCustomerDto, CreateCustomerArguments>(request, body);

            var result = resp != null
                            ? resp.Customer
                            : null;

            return result;
        }
    }
}