using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReadleApp.Domain.Model
{
    public class OpenLibraryModel
    {
        public int Id { get; set; }
        [JsonPropertyName("key")]
        public string? WorkKey { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("cover_i")]
        public int? CoverIdAlt
        {
            get => CoverIdAt;
            set => CoverIdAt = value;
        }
        [JsonPropertyName("cover_id")]
        public int? CoverIdAt { get; set; }
        public string? CoverUrl
        {
            get
            {
                int? coverId = CoverIdAt ?? CoverIdAlt ?? Covers?.FirstOrDefault();
                return coverId.HasValue ? $"https://covers.openlibrary.org/b/id/{coverId}-L.jpg" : null;
            }
        }


        [JsonPropertyName("covers")]
        public List<int>? Covers { get; set; }

        [JsonPropertyName("edition_count")]
        public int? EditionCount { get; set; }



        [JsonPropertyName("subject")]
        public List<string>? Subjects { get; set; }


        public string AuthorsString()
        {
           return  Authors != null ? string.Join(", ", Authors.Select(a => a.Author!.Name)) : string.Empty;
        }

        [JsonPropertyName("authors")]
        public List<WorkAuthor>? Authors { get; set; }

        public class WorkAuthor
        {
            [JsonPropertyName("author")]
            public AuthRef? Author { get; set; }

    
        }

        public class AuthRef
        {
            [JsonPropertyName("key")]
            public string? Key { get; set; }
            [JsonIgnore]
            public string? Name { get; set; }
        }





        public string SubjectsString =>
            Subjects != null ? string.Join(", ", Subjects) : string.Empty;


        public string? Category { get; set; }



    }



}
