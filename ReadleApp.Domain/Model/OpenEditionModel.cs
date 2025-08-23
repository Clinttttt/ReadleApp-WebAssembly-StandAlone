using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Model
{
    public class Edition
    {
       
        [JsonPropertyName("publish_date")]
        public string? PublishDate { get; set; }

        // Formats available (plain text, PDF, EPUB, etc.)
        [JsonPropertyName("formats")]
        public Dictionary<string, string>? Formats { get; set; }

    
        [JsonPropertyName("availability")]
        public string? Availability { get; set; }


        // (optional)
        [JsonPropertyName("key")]
        public string? EditionKey { get; set; }

    
        [JsonPropertyName("identifiers")]
        public Identifiers? Identifier { get; set; }

        public string? Category { get; set; }

        [JsonIgnore]
        public string? WorkKeys { get; set; }

    }
}
