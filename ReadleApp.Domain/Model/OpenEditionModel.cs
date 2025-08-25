using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static ReadleApp.Domain.Model.OpenLibraryModel;

namespace ReadleApp.Domain.Model
{
    public class Edition
    {

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

        [JsonPropertyName("by_statement")]
        public string? ByStatement { get; set; }

        [JsonPropertyName("languages")]
        public List<LanguageNames>? Language { get; set; }

        [JsonPropertyName("publishers")]
        public List<string>? Publisher { get; set; }
        [JsonPropertyName("isbn_13")]
        public List<string>? ISBN { get; set; }

        [JsonPropertyName("publish_date")]
        public string? PublishedDate { get; set; }

        [JsonPropertyName("subtitle")]
        public string? SubTitle { get; set; }

        [JsonPropertyName("series")]
        public List<string>? Series { get; set; }

        [JsonPropertyName("publish_places")]
        public List<string>? PublishedPlace { get; set; }
        [JsonPropertyName("ocaid")]
        public string? OCAID { get; set; }
  

    }



}
