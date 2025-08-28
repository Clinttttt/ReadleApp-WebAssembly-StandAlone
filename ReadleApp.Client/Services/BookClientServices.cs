using ReadleApp.Domain;
using ReadleApp.Domain.Model;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using static ReadleApp.Domain.Model.OpenLibraryModel;

namespace ReadleApp.Client.Services
{
    public class BookClientServices
    {
        private readonly HttpClient _http;
        public BookClientServices(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<OpenLibraryPreviewDetails>> MostReadAsync()
        {
                                                                     
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/MostRead")
                   ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<List<OpenLibraryPreviewDetails>> AdventureAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/Adventure")
                ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<List<OpenLibraryPreviewDetails>> RomanceAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/Romance")
                ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<List<OpenLibraryPreviewDetails>> ScienceAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/Science")
                ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<List<OpenLibraryPreviewDetails>> MysteryAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/Mystery")
                ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<List<OpenLibraryPreviewDetails>> ChildrenAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/Children")
                ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<List<OpenLibraryPreviewDetails>> PoetryAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/Poetry")
                ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<List<OpenLibraryPreviewDetails>> HistoryAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/History")
            ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<List<OpenLibraryPreviewDetails>> ShortStoriesAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/ShortStories")
                ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<List<OpenLibraryPreviewDetails>> ClassicsAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryPreviewDetails>>("api/Books/Classics")
                ?? new List<OpenLibraryPreviewDetails>();
        }
        public async Task<string> BookCover(string title, string author)
        {
            return await _http.GetStringAsync($"api/Books/BookCover?title={Uri.EscapeDataString(title)}&author={Uri.EscapeDataString(author)}");
        }
        public async Task<OpenLibraryDoc?> GetBookById(string workkey)
        {
            return await _http.GetFromJsonAsync<OpenLibraryDoc>($"api/Books/GetBookById/{workkey}");

        }
  
    
}
}
