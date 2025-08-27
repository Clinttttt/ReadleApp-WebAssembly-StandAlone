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
        public async Task<List<OfflineReadingModel>> MostReadAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/MostRead")
                   ?? new List<OfflineReadingModel>();
        }
        public async Task<List<OfflineReadingModel>> AdventureAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/Adventure")
                ?? new List<OfflineReadingModel>();
        }
        public async Task<List<OfflineReadingModel>> RomanceAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/Romance")
                ?? new List<OfflineReadingModel>();
        }
        public async Task<List<OfflineReadingModel>> ScienceAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/Science")
                ?? new List<OfflineReadingModel>();
        }
        public async Task<List<OfflineReadingModel>> MysteryAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/Mystery")
                ?? new List<OfflineReadingModel>();
        }
        public async Task<List<OfflineReadingModel>> ChildrenAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/Children")
                ?? new List<OfflineReadingModel>();
        }
        public async Task<List<OfflineReadingModel>> PoetryAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/Poetry")
                ?? new List<OfflineReadingModel>();
        }
        public async Task<List<OfflineReadingModel>> HistoryAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/History")
            ?? new List<OfflineReadingModel>();
        }
        public async Task<List<OfflineReadingModel>> ShortStoriesAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/ShortStories")
                ?? new List<OfflineReadingModel>();
        }
        public async Task<List<OfflineReadingModel>> ClassicsAsync()
        {
            return await _http.GetFromJsonAsync<List<OfflineReadingModel>>("api/Books/Classics")
                ?? new List<OfflineReadingModel>();
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
