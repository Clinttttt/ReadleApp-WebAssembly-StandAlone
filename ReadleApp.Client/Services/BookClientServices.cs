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
        public async Task<List<BookGutendex>> MostReadAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/MostRead")
                   ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> AdventureAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/Adventure")
                ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> RomanceAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/Romance")
                ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> ScienceAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/Science")
                ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> MysteryAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/Mystery")
                ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> ChildrenAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/Children")
                ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> PoetryAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/Poetry")
                ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> HistoryAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/History")
            ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> ShortStoriesAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/ShortStories")
                ?? new List<BookGutendex>();
        }
        public async Task<List<BookGutendex>> ClassicsAsync()
        {
            return await _http.GetFromJsonAsync<List<BookGutendex>>("api/Books/Classics")
                ?? new List<BookGutendex>();
        }
        public async Task<string> BookCover(string title, string author)
        {
            return await _http.GetStringAsync($"api/Books/BookCover?title={Uri.EscapeDataString(title)}&author={Uri.EscapeDataString(author)}");
        }
    }
}
