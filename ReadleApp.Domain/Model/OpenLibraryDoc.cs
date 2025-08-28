using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using static ReadleApp.Domain.Model.OpenLibraryBookShelves;
using System.Globalization;

namespace ReadleApp.Domain.Model
{
    public class OpenLibraryDoc
    {
        public int Id { get; set; }

        [JsonPropertyName("key")]
        public string? WorkKey { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("author_key")]
        public List<string>? AuthorKey { get; set; }
        [JsonPropertyName("author_name")]
        public List<string>? AuthorName { get; set; }

        [JsonPropertyName("cover_i")]
        public int CoverKey { get; set; }

        [JsonPropertyName("has_fulltext")]
        public bool HasFullText { get; set; }

        [JsonPropertyName("ebook_access")]
        public string? EbookAccess { get; set; }

        [JsonPropertyName("ia")]
        public List<string>? IA { get; set; }

        [JsonPropertyName("subtitle")]
        public string? SubTitle { get; set; }

        [JsonPropertyName("language")]
        public List<string>? Languages { get; set; }

        [JsonIgnore]
        public string? Category { get; set; }
        public string? PublishedDateClone { get; set; }
        public List<string>? PublishersClone { get; set; }
        public List<string>? SubjectsClone { get; set; }
        public Bookshelve? BookshelveClone { get; set; }
        // public string SubjectsString => SubjectsClone != null ? string.Join(",", SubjectsClone.Take(5)) : string.Empty;
        [JsonIgnore]
        public object? DescriptionClones { get; set; }

        public string? DescriptionHelper
        {
            get
            {
                if (DescriptionClones is not null && DescriptionClones is JsonElement element)
                {
                    if (element.ValueKind == JsonValueKind.String) return element.GetString();
                    if (element.ValueKind == JsonValueKind.Object && element.TryGetProperty("value", out var val))
                        return val.GetString();


                }

                return DescriptionClones?.ToString() ?? string.Empty;
            }
        }
        public string? WorkString => WorkKey != null ? WorkKey!.Replace("/works/", "") ?? "" : null;


        public string? CoverUrl
        {
            get
            {
                int? coverId = CoverKey;
                return coverId.HasValue ? $"https://covers.openlibrary.org/b/id/{coverId}-L.jpg" : null;
            }
        }
        public string Substring => SubjectsClone != null ? string.Join(",", SubjectsClone.Take(5)) : string.Empty;



        [JsonIgnore]
        public string? FullText { get; set; }

    }

}
