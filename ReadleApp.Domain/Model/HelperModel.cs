using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static ReadleApp.Domain.Model.HelperModel;

namespace ReadleApp.Domain.Model
{
    public class HelperModel
    {
     
    }
 
    public class Identifiers
    {
        [JsonPropertyName("isbn_13")]
        public List<string>? Isbn13 { get; set; }
    }
    public class Ebook
    {
        [JsonPropertyName("availability")]
        public string? Availability { get; set; }

    
    }
  
    public class Author
    {
        [JsonPropertyName("author")]
        public AuthorREf? author { get; set; }
    }
    public class AuthorREf
    {
        [JsonPropertyName("Key")]
        public string? Key { get; set; }
    }

    public class AuthorNames
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("Key")]
        public string? Key { get; set; }
    }
   
    public class LanguageNames
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }
    }


}
