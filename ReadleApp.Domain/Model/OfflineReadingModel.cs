using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Model
{
  public class OfflineReadingModel
    {
        public int _Id { get; set; }
        public string? _Workkey { get; set; }
        public string? _Title { get; set; }
        public List<string>? _AuthorName { get; set; }
        [JsonIgnore]
        public string? _FullText { get; set; }
        public string? _Description { get; set; }
        public string? _IA { get; set; }
      
        public string? _CoverBase64 { get; set; }
        [JsonIgnore]
        public string? Category { get; set; }
        [JsonIgnore]
        public string? WorkString => _Workkey != null ? _Workkey!.Replace("/works/", "") ?? "" : null;

    }
}
