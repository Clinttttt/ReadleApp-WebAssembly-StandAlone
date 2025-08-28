using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Model
{
   public class OpenLibraryPreviewDetails
    {
        public int Id { get; set; }
        public string? _Workkey { get; set; }
        //public string? _Title { get; set; }
        //public List<string>? AuthorName { get; set; }
        public int _coverkey { get; set; }
        [JsonIgnore]
        public string? Category { get; set; }

        public string? WorkString => _Workkey != null ? _Workkey!.Replace("/works/", "") ?? "" : null;

        public string? _CoverBase64
        {
            get
            {
                int? key = _coverkey;
                return key.HasValue ? $"https://covers.openlibrary.org/b/id/{key}-L.jpg" : null;
            }
        }

    }
}
