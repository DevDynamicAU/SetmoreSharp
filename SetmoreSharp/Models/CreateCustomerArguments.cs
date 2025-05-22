using System.Text.Json.Serialization;

namespace SetmoreSharp.Models
{
    public class CreateCustomerArguments
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email_id")]
        public string Email { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("cell_phone")]
        public string Mobile { get; set; }

        [JsonPropertyName("work_phone")]
        public string WorkPhone { get; set; }

        [JsonPropertyName("home_phone")]
        public string HomePhone { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostCode { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("additional_fields")]
        public CustomerAdditionalFields AdditionalFields { get; set; }
    }
}