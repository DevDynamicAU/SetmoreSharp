using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SetmoreSharp.Models
{
    public class ServicesDto
    {
        [JsonPropertyName("services")]
        public List<Service> Services { get; set; }
    }
}