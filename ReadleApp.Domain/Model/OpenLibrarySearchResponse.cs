using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Model
{
   public class OpenLibrarySearchResponse
    {
        public List<OpenLibraryDoc>? Docs { get; set; } = new();
      

    }
}
