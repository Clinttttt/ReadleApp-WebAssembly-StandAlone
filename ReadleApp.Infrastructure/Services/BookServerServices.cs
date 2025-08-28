using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ReadleApp.Infrastructure.Services
{
    public class BookServerServices
    {
        private readonly HttpClient _http;
        public BookServerServices( HttpClient http)
        {
            _http = http;
        }
        public async Task<string?> GetFulltext(string fulltext)
        {
           return await _http.GetStringAsync($"https://localhost:7033/api/Books/Fulltext/{fulltext}");

        }
        public async Task<OpenLibraryModel?> GetDetails(string workkey)
        {
            return await _http.GetFromJsonAsync<OpenLibraryModel>($"https://localhost:7033/api/Books/GetDetails/{workkey}");
        }
        public async Task<string> GetBase64(int cover)
        {
            return await _http.GetStringAsync($"https://localhost:7033/api/Books/Cover/{cover}");
        }

    } 
}
