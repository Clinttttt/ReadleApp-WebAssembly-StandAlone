using ReadleApp.Domain;
using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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

     

            public async Task<OpenLibraryModel?> GetBookAsync(string workkey)
        { var responsework = await _http.GetFromJsonAsync<OpenLibraryModel>($"https://openlibrary.org/works/{workkey}.json");
            var editionsResponse = await _http.GetFromJsonAsync<OpenEditionResponse>($"https://openlibrary.org/works/{workkey}/editions.json");
            if (editionsResponse is not null) 
            {
                foreach (var editionkey in editionsResponse!.Entries!) 
                { editionkey.WorkKeys = responsework!.WorkKey;
                }
                responsework!.Entries = editionsResponse.Entries;
            } return responsework;
        }



        public async Task<List<OpenLibraryModel>?> MostReadBookAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/fantasy.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }
    

        public async Task<List<OpenLibraryModel>> AdventureAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/adventure.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }
        public async Task<List<OpenLibraryModel>> RomanceAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/romance.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }

        public async Task<List<OpenLibraryModel>> ScienceAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/science.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }


        public async Task<List<OpenLibraryModel>> MysteryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/mystery.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }
        public async Task<List<OpenLibraryModel>> ChildrenAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/children.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }


        public async Task<List<OpenLibraryModel>> PoetryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/poetry.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }
        public async Task<List<OpenLibraryModel>> HistoryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/history.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }

        public async Task<List<OpenLibraryModel>> ShortStoriesAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/short_stories.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }


        public async Task<List<OpenLibraryModel>> ClassicsAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/classic_literature.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }








        /*     
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
             }*/






        /*    public async Task<OpenLibraryModel?> GetBookEdition(string workkey)
        {
            return await _http.GetFromJsonAsync<OpenLibraryModel>($"https://openlibrary.org/works/{workkey}/editions.json");
        }*/
    }
}
