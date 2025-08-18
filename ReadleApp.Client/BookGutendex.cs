using System.Text.Json.Serialization;

namespace ReadleApp.Client
{
    public class BookGutendex
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public int BookId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("languages")]
        public List<string>? Languages { get; set; }

        [JsonPropertyName("authors")]
        public List<Author>? Authors { get; set; }

        [JsonPropertyName("subjects")]
        public List<string>? Subjects { get; set; }

        [JsonPropertyName("bookshelves")]
        public List<string>? BookShelves { get; set; }

        [JsonPropertyName("summaries")]
        public List<string>? Summaries { get; set; }

        [JsonPropertyName("download_count")]
        public int? DownloadCount { get; set; }

        [JsonPropertyName("formats")]
        public Dictionary<string, string>? Format { get; set; }

        [JsonPropertyName("coverUrl")]
        public string? CoverUrl { get; set; }


        public string? Content { get; set; }

        public class Author
        {
            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [JsonPropertyName("birth_year")]
            public int? BirthYear { get; set; }

            [JsonPropertyName("death_year")]
            public int? DeathYear { get; set; }
        }
       
    }
    public class GutendexResponse
    {
        public List<BookGutendex>? Results { get; set; }
    }
}
