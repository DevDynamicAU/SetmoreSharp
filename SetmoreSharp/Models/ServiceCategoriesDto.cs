using System.Text.Json.Serialization;

namespace SetmoreSharp.Models
{
    internal class ServiceCategoriesDto
    {
        [JsonPropertyName("service_categories")]
        public List<ServiceCategory> ServiceCategories { get; set; }
    }
}