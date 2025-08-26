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
        public async Task<List<OpenLibraryDoc>> MostReadAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/MostRead")
                   ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> AdventureAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/Adventure")
                ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> RomanceAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/Romance")
                ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> ScienceAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/Science")
                ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> MysteryAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/Mystery")
                ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> ChildrenAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/Children")
                ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> PoetryAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/Poetry")
                ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> HistoryAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/History")
            ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> ShortStoriesAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/ShortStories")
                ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> ClassicsAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryDoc>>("api/Books/Classics")
                ?? new List<OpenLibraryDoc>();
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
