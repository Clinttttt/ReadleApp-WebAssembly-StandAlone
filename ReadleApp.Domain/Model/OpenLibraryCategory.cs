using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Model
{
  public class OpenLibraryCategory
    {
        public int Id { get; set; }
        public string? Workkey { get; set; }
        public string? IA { get; set; }
 
        public string? CoverUrl { get; set; }
    }
}
