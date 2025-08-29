using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Json;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;
using static ReadleApp.Domain.Model.OpenLibraryBookShelves;

namespace ReadleApp.Domain.Model
{
    public class OpenLibraryModel
    {
        //Primary
        public int Id { get; set; }

        //key
        [JsonPropertyName("key")]
        public string? WorkKey { get; set; }

        [MaxLength]
        public string? FullText { get; set; }

        [JsonPropertyName("subjects")]
        public List<string>? Subject { get; set;}

        [JsonPropertyName("description")]
        public object? DescriptionRaw { get; set; }

        [JsonPropertyName("subtitle")]
        public string? Subtitle { get; set; }
        public int? CoverKey { get; set; }
        [JsonPropertyName("authors")]
        public List<Author>? Authors { get; set; }
    }














    /*
        [JsonPropertyName("entries")]
        public List<Edition>? Entries { get; set; }

        public HelperObject? ObjectHelper { get; set; }


        //covers
        [JsonPropertyName("cover_i")]
        public int? CoverIdAlt
        {
            get => CoverIdAt;
            set => CoverIdAt = value;
        }
        [JsonPropertyName("cover_id")]
        public int? CoverIdAt { get; set; }

        //edition count
        [JsonPropertyName("edition_count")]
        public int? EditionCount { get; set; }

        [JsonIgnore]
        public string? Category { get; set; }





        //subject helper
        public string SubjectsString =>
        Subjects != null  ? string.Join(", ", Subjects.Take(5)) : string.Empty;

        [JsonIgnore]
        public string? Isbn13 => BookEdition?.Identifier?.Isbn13 != null && BookEdition.Identifier.Isbn13.Count > 0
           ? BookEdition.Identifier.Isbn13[0]
           : null;
        [JsonIgnore]
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
        [JsonPropertyName("source_records")]
        public List<string>? SourceRecord { get; set; }

     
        //working 
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("covers")]
        public List<int>? Covers { get; set; }
        public string WorkString => WorkKey!.Replace("/works/", "") ?? "";
        public string? CoverUrl
        {
            get
            {
                int? coverId = CoverIdAt ?? CoverIdAlt ?? Covers?.FirstOrDefault();
                return coverId.HasValue ? $"https://covers.openlibrary.org/b/id/{coverId}-L.jpg" : null;
            }
        }
        [JsonPropertyName("bookshelves")]
        public Bookshelve? Bookshelves { get; set; }

        public string? AuthorName { get; set; }

        [JsonPropertyName("authors")]
        public List<Author>? Authors { get; set; }
        [JsonPropertyName("subject_people")]
        public List<string>? SubjectPeople { get; set; }
        [JsonPropertyName("subject_places")]
        public List<string>? SubjectPlaces { get; set; }
        [JsonPropertyName("subject_times")]
        public List<string>? SubjectTime { get; set; }
        [JsonPropertyName("latest_revision")]
        public int LatestRivision { get; set; }

        public List<string>? Languagess { get; set; }
        [JsonPropertyName("subjects")]
        public List<string>? Subjects { get; set; }
        public List<string>? Publisher { get; set; }
        public List<string>? ISBN { get; set; }
        public string? PubLishedDate { get; set; }
        public string? SubTitle { get; set; }
        public List<string>? Series { get; set; }
        public List<string>? PublishedPlace { get; set; }
        public string? OCAID { get; set; }*/




















}
