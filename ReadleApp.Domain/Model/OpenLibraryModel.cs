using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReadleApp.Domain.Model
{
    public class OpenLibraryModel
    {
        //Primary
        public int Id { get; set; }

        //key
        [JsonPropertyName("key")]
        public string? WorkKey { get; set; }

        [JsonPropertyName("entries")]
        public List<Edition>? Entries { get; set; }

        public HelperObject? ObjectHelper { get; set; }


        //Languages
        [JsonPropertyName("languages")]
        public List<LanguageRef>? languages { get; set; }



        //subjects
        [JsonPropertyName("subject")]
        public List<string>? Subjects { get; set; }

        //Text
        [JsonPropertyName("ebooks")]
        public List<Ebook>? Ebooks { get; set; }


        [JsonPropertyName("size")]
        public int Size { get; set; }

        //Title
        [JsonPropertyName("title")]
        public string? Title { get; set; }

    

            [JsonPropertyName("authors")]
            public List<Author>? Authors { get; set; }

            [JsonIgnore]
            public string? AuthorNames => Authors == null || Authors.Count == 0
                ? null
                : string.Join(", ", Authors.Select(a => a.Name));
        





        //covers
        [JsonPropertyName("cover_i")]
        public int? CoverIdAlt
        {
            get => CoverIdAt;
            set => CoverIdAt = value;
        }
        [JsonPropertyName("cover_id")]
        public int? CoverIdAt { get; set; }

        [JsonPropertyName("covers")]
        public List<int>? Covers { get; set; }


        //edition count
        [JsonPropertyName("edition_count")]
        public int? EditionCount { get; set; }

        public string? Category { get; set; }

      
        //workkey helper
        public string WorkString => WorkKey!.Replace("/works/", "") ?? "";

        //cover helper
        public string? CoverUrl
        {
            get
            {
                int? coverId = CoverIdAt ?? CoverIdAlt ?? Covers?.FirstOrDefault();
                return coverId.HasValue ? $"https://covers.openlibrary.org/b/id/{coverId}-L.jpg" : null;
            }
        }

        //subject helper
        public string SubjectsString =>
        Subjects != null ? string.Join(", ", Subjects) : string.Empty;


        [JsonIgnore]
        public string? Isbn13 => BookEdition?.Identifier?.Isbn13 != null && BookEdition.Identifier.Isbn13.Count > 0
           ? BookEdition.Identifier.Isbn13[0]
           : null;
        public Edition? BookEdition { get; set; }


        //description
        [JsonPropertyName("description")]
        public object? DescriptionRaw { get; set; }
        [JsonIgnore]
        public string? Description
        {
            get
            {
                if (DescriptionRaw is not null && DescriptionRaw is JsonElement element)
                {
                    if (element.ValueKind == JsonValueKind.String) return element.GetString();
                    if (element.ValueKind == JsonValueKind.Object && element.TryGetProperty("value", out var val))
                        return val.GetString();
                }
                return DescriptionRaw?.ToString();
            }
        }
    
            

        }


























    
}
