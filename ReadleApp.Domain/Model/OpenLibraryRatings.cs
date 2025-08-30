using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Model
{
    public class OpenLibraryRatings
    {
        [JsonPropertyName("counts")]
        public Rating? Ratings { get; set; }
        public class Rating
        {
            [JsonPropertyName("1")]
            public int One { get; set; }
            [JsonPropertyName("2")]
            public int Two { get; set; }
            [JsonPropertyName("3")]
            public int Three { get; set; }
            [JsonPropertyName("4")]
            public int Four { get; set; }
            [JsonPropertyName("5")]
            public int Five { get; set; }
        }
        [JsonPropertyName("summary")]
        public Summary? summary { get; set; }
        public class Summary
        {
            [JsonPropertyName("average")]
            public double average { get; set; }
            [JsonPropertyName("count")]
            public int count { get; set; }

            [JsonPropertyName("sortable")]
            public double Sortable { get; set; }
        }
    }
   
  
}
