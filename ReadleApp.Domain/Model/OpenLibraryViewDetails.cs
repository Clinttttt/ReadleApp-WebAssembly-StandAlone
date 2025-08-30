using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static ReadleApp.Domain.Model.OpenLibraryBookShelves;
using static ReadleApp.Domain.Model.OpenLibraryRatings;

namespace ReadleApp.Domain.Model
{
   public class OpenLibraryViewDetails
    {
        public int Id { get; set; }
        public string? Workkey { get; set; }
        public string? Title { get; set; }
        public List<string>? Authorname { get; set; }
        public int Coverkey { get; set; }
        public string? SubTitle { get; set; }
        public string? Publishdate { get; set; }
        public object? Description { get; set; }
        public List<string>? Subjects { get; set; }
        public string[]? Publishers { get; set; }
        public Bookshelve? Bookshelves { get; set; }
        public Rating? Rating { get; set; }
        public Summary? Summary { get; set; }
        public string? FullText { get; set; }

        public string SummaryHelper => Summary != null ? string.Join(",", Summary) : string.Empty;
        public string RatingHelper => Rating != null ? string.Join(",", Rating) : string.Empty;
        public string AuthorHelper => Authorname != null ? string.Join(",", Authorname) : string.Empty;
        public string Substring => Subjects != null ? string.Join(",", Subjects.Take(5)) : string.Empty;
        public string PublisherHelper => Publishers != null ? string.Join(",", Publishers) : string.Empty;
        public string? WorkString => Workkey != null ? Workkey!.Replace("/works/", "") ?? "" : null;

        public string? _CoverBase64
        {
            get
            {
                int? key = Coverkey;
                return key.HasValue ? $"https://covers.openlibrary.org/b/id/{key}-L.jpg" : null;
            }
        }
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
        public string? CoverHelper
        {
            get
            {
                int? key = Coverkey;
                return key.HasValue ? $"https://covers.openlibrary.org/b/id/{key}-L.jpg" : null;
            }
        }
    }
}
