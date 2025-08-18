using System.Net.Http.Json;

namespace ReadleApp.Client
{
    public class BookApi
    {
        private readonly HttpClient _http;
        public BookApi (HttpClient http)
        {
            _http = http;
        }
        public async Task<List<BookGutendex>> MostRead()
        {
            var Request = await _http.GetFromJsonAsync<GutendexResponse>("https://gutendex.com/books/?sort=download_count&page_size=10");
            return Request?.Results ?? new List<BookGutendex>();
        }
    }
}
