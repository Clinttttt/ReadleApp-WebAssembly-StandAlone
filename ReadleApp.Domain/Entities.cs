using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static ReadleApp.Domain.Model.OpenLibraryBookShelves;

namespace ReadleApp.Domain
{
   public class OpenLibraryEntities
    {
        public int Id { get; set; }
        public string? Workkey { get; set; }
        public List<string>? Subject { get; set; }
        public string? Title { get; set; }
        public List<string>? AuthorName { get; set; }
        public string? IA { get; set; }
        public int? CoverKey { get; set; }
        public string? SubTitle { get; set; }
        [JsonIgnore]
        public string? Category { get; set; }
        public string? PublishedDate { get; set; }
        public List<string>? Publishers { get; set; }
        public List<string>? Languages { get; set; }
        public Bookshelve? GetBookshelve { get; set; }
        [JsonIgnore]
        public string? FullText { get; set; }
        public object? Description { get; set; }
        [JsonIgnore]
        public string? DescriptionHelper
        {
            get
            {
                if (Description is not null && Description is JsonElement element)
                {
                    if (element.ValueKind == JsonValueKind.String) return element.GetString();
                    if (element.ValueKind == JsonValueKind.Object && element.TryGetProperty("value", out var val))
                        return val.GetString();


                }

                return Description?.ToString() ?? string.Empty;
            }
        }
     public async Task<string?> ConverImageBase64String(int? coverkey )
        {
            using var http = new HttpClient();
            if (!coverkey.HasValue) return null;

            var url =   $"https://covers.openlibrary.org/b/id/{coverkey}-L.jpg" ;
            var convertbase64string = await http.GetByteArrayAsync(url);
            return  Convert.ToBase64String(convertbase64string);
        }
     
   

        public string? WorkString => Workkey != null ? Workkey!.Replace("/works/", "") ?? "" : null;


        public string Substring => Subject != null ? string.Join(",", Subject.Take(5)) : string.Empty;











    }

}
