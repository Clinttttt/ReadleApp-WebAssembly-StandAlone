using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Model
{
    public class OpenLibraryDoc
    {
        [JsonPropertyName("cover_i")]
        public int CoverId { get; set; }
        [JsonPropertyName("edition_count")]
        public int EditionCount { get; set; }
    }
}
