using ReadleApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Model
{
    public class OpenLibraryResponse
    {
        [JsonPropertyName("works")]
        public List<OpenLibraryModel>? Works { get; set; }


        [JsonPropertyName("docs")]
        public List<OpenLibraryModel>? Docs { get; set; }
    }
}
