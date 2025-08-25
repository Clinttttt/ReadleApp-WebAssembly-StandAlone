using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Model
{
  public class OpenLibraryBookShelves
    {
        [JsonPropertyName("key")]
        public string? WorkKey { get; set; }

        [JsonPropertyName("counts")]
        public Bookshelve? GetBookshelves { get; set; }
        public class Bookshelve
        {
            [JsonPropertyName("want_to_read")]
            public int WantToRead { get; set; }

            [JsonPropertyName("currently_reading")]
            public int CurrentReading { get; set; }

            [JsonPropertyName("already_read")]
            public int AlreadyRead { get; set; }

        }
    }
}
