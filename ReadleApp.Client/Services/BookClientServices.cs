using ReadleApp.Domain.Model;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace ReadleApp.Client.Services
{
    public class BookClientServices
    {
        private readonly HttpClient _http;
        public BookClientServices(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<OpenLibraryModel>> MostReadAsync()
        {
            return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/MostRead")
                   ?? new List<OpenLibraryModel>();
        }
        public async Task<List<OpenLibraryModel>> AdventureAsync()
            {
                return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/Adventure")
                    ?? new List<OpenLibraryModel>();
            }
            public async Task<List<OpenLibraryModel>> RomanceAsync()
            {
                return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/Romance")
                    ?? new List<OpenLibraryModel>();
            }
            public async Task<List<OpenLibraryModel>> ScienceAsync()
            {
                return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/Science")
                    ?? new List<OpenLibraryModel>();
            }
            public async Task<List<OpenLibraryModel>> MysteryAsync()
            {
                return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/Mystery")
                    ?? new List<OpenLibraryModel>();
            }
            public async Task<List<OpenLibraryModel>> ChildrenAsync()
            {
                return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/Children")
                    ?? new List<OpenLibraryModel>();
            }
            public async Task<List<OpenLibraryModel>> PoetryAsync()
            {
                return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/Poetry")
                    ?? new List<OpenLibraryModel>();
            }
            public async Task<List<OpenLibraryModel>> HistoryAsync()
            {
                return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/History")
                ?? new List<OpenLibraryModel>();
            }
            public async Task<List<OpenLibraryModel>> ShortStoriesAsync()
            {
                return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/ShortStories")
                    ?? new List<OpenLibraryModel>();
            }
            public async Task<List<OpenLibraryModel>> ClassicsAsync()
            {
                return await _http.GetFromJsonAsync<List<OpenLibraryModel>>("api/Books/Classics")
                    ?? new List<OpenLibraryModel>();
            }
            public async Task<string> BookCover(string title, string author)
            {
                return await _http.GetStringAsync($"api/Books/BookCover?title={Uri.EscapeDataString(title)}&author={Uri.EscapeDataString(author)}");
            }
        public async Task<OpenLibraryModel?> GetBookById(string id)
        {
           return await _http.GetFromJsonAsync<OpenLibraryModel>($"api/Books/GetBookById/{id}");
             
        } 
    }
}
