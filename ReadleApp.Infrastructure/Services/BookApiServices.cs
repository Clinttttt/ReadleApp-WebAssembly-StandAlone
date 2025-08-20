using ReadleApp.Domain;
using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ReadleApp.Infrastructure.Services
{
    public class BookApiServices
    {
        private readonly Dictionary<string, string> _coverCache = new();
        private readonly HttpClient _http;
        public BookApiServices(HttpClient http)
        {
            _http = http;
        }
        public async Task<string?> GetOpenLibraryCoverAsync(string title, string author)
        {
            try
            {
                var key = $"{title}-{author}".ToLower();
                if (_coverCache.TryGetValue(key, out var cachedCover))
                    return cachedCover;

                var url = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(title)}&author={Uri.EscapeDataString(author)}&limit=6";
                var response = await _http.GetFromJsonAsync<OpenLibrarySearchResponse>(url);

                var doc = response?.Docs?
                 .Where(d => d.CoverId > 0)
                 .OrderByDescending(d => d.EditionCount) 
                 .FirstOrDefault();

                string? cover = null;
                if (doc != null && doc.CoverId > 0)
                {
                    cover = $"https://covers.openlibrary.org/b/id/{doc.CoverId}-L.jpg";
                }

                _coverCache[key] = cover ?? "";
                return cover;
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<BookGutendex>> FetchBooksAsync(string url)
        {
            var response = await _http.GetFromJsonAsync<GutendexResponse>(url);
            var results = response!.Results ?? new List<BookGutendex>();

            foreach (var book in results)
            {
               var author = book.Authors?.FirstOrDefault()?.Name ?? "";
                book.CoverUrl = await GetOpenLibraryCoverAsync(book.Title!, author)
                    ?? book.Format!.FirstOrDefault(b => b.Key.Contains("image")).Value ?? "";

            }
            return results;
        }

        public async Task<BookGutendex?> GetBookAsync(int Id)
        {
            return await _http.GetFromJsonAsync<BookGutendex>($"https://gutendex.com/books/{Id}");
        }
        public async Task<List<BookGutendex>> MostReadBookAsync() 
            => await FetchBooksAsync ("https://gutendex.com/books/?sort=download_count&page_size=10");
      
        
        public async Task<List<BookGutendex>> AdventureAsync()
            => await FetchBooksAsync("https://gutendex.com/books?topic=adventure&page_size=10");
              
        public async Task<List<BookGutendex>> RomanceAsync()

             => await FetchBooksAsync("https://gutendex.com/books?topic=romance&page_size=10");
           
        
        public async Task<List<BookGutendex>> ScienceAsync()
        
            => await FetchBooksAsync ("https://gutendex.com/books?topic=science&page_size=10");
           
        
        public async Task<List<BookGutendex>> MysteryAsync()
        
              => await FetchBooksAsync("https://gutendex.com/books?topic=mystery&page_size=10");
           
        
        public async Task<List<BookGutendex>> ChildrenAsync()
        
            => await FetchBooksAsync("https://gutendex.com/books?topic=Children's&page_size=10");
   
        
        public async Task<List<BookGutendex>> PoetryAsync()
        
          => await FetchBooksAsync("https://gutendex.com/books?topic=Poetry&page_size=10");
           
        
        public async Task<List<BookGutendex>> HistoryAsync()
        
         => await FetchBooksAsync("https://gutendex.com/books?topic=History&page_size=10");
         
        public async Task<List<BookGutendex>> ShortStoriesAsync()
        
             => await FetchBooksAsync("https://gutendex.com/books?topic=Short%20Stories&page_size=10");
         
        
        public async Task<List<BookGutendex>> ClassicsAsync()
        
            => await FetchBooksAsync("https://gutendex.com/books?topic=Classics&page_size=10");
        
        public async Task<List<BookGutendex>> GetBookByTopicAsnyc(string topic)
        {
            var encodedTopic = Uri.EscapeDataString(topic);
            var response = await _http.GetFromJsonAsync<GutendexResponse>($"https://gutendex.com/books?topic={encodedTopic}&page_size=10");
            return response?.Results ?? new List<BookGutendex>();
        }
    }
}
