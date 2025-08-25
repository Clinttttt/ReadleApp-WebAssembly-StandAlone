using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static ReadleApp.Domain.Model.OpenLibraryModel;

namespace ReadleApp.Domain.Model
{
   public class OpenLibrarySearchResponse
    {
        public List<OpenLibraryDoc>? Docs { get; set; } = new();

        public List<int>? Covers { get; set; }
    }
}
