using System.Text.Json.Serialization;

namespace SetmoreSharp.Models
{
    public class Customer
    {
        [JsonIgnore]
        public string Id => Key;

        [JsonPropertyName("key")]
        public string Key { get; set; }

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

        [JsonIgnore]
        public string FullMobile => $"{(CountryCode.HasValue() ? $"+{CountryCode}" : "")} {Mobile}".Trim();

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostCode { get; set; }

        [JsonPropertyName("company_key")]
        public string CompanyKey { get; set; }

        [JsonPropertyName("contact_type")]
        public string ContactType { get; set; }

        [JsonPropertyName("additional_fields")]
        public CustomerAdditionalFields AdditionalFields { get; set; }
    }
}