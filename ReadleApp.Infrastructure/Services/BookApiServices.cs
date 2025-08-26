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
using static ReadleApp.Domain.Model.Edition;
using static ReadleApp.Domain.Model.HelperModel;
using static ReadleApp.Domain.Model.OpenLibraryModel;
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



        public async Task<OpenLibraryResponse?> GetBookAsync(string workkey)
        {
            var responsework = await _http.GetFromJsonAsync<OpenLibraryModel>($"https://openlibrary.org/works/{workkey}.json");
            var editionsResponse = await _http.GetFromJsonAsync<OpenEditionResponse>($"https://openlibrary.org/works/{workkey}/editions.json");
            var bookshelvesResponse = await _http.GetFromJsonAsync<OpenLibraryBookShelves>($"https://openlibrary.org/works/{workkey}/bookshelves.json");
            var responsedoc = await _http.GetFromJsonAsync<OpenLibraryResponse>($"https://openlibrary.org/search.json?q={workkey}");
            //var firstDoc = responsedoc.Docs.FirstOrDefault( s => s.WorkKey == workkey);

            var FirstDoc = responsedoc!.Docs!.FirstOrDefault(s => s.IA != null && s.IA.Any());
            var GetAi = FirstDoc!.IA!.FirstOrDefault();
            try
            {


                var FetchIa = await _http.GetStringAsync($"https://archive.org/stream/{GetAi}/{GetAi}_djvu.txt");
                foreach (var response in responsedoc.Docs!)
                {
                    response.FullText = FetchIa;
                }
              
                Console.Write($"FullText{responsedoc.Docs.Select( s => s.FullText)}");

            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                responsework!.FullText = "Full text not available.";
            }

            if (editionsResponse is not null)
            {
                foreach (var editionkey in editionsResponse!.Entries!)
                {
                    editionkey.WorkKeys = responsework!.WorkKey;
                }
                if (editionsResponse is not null || bookshelvesResponse is not null)
                {
                    foreach (var ResponseDoc in responsedoc.Docs!)
                    {
                        bookshelvesResponse!.WorkKey = responsework!.WorkKey;
                        ResponseDoc.BookshelveClone = bookshelvesResponse.GetBookshelves;
                        ResponseDoc.PublishedDateClone = editionsResponse!.Entries.Select(s => s.PublishedDate).FirstOrDefault(s => !string.IsNullOrEmpty(s));
                        ResponseDoc.PublishersClone = editionsResponse.Entries.Where(s => s.Publisher != null).SelectMany(s => s.Publisher!).ToList();
                        ResponseDoc.SubjectsClone = responsework.Subject;
                        ResponseDoc.DescriptionClones = responsework.DescriptionRaw;
                    }
                  
                }
       
             
            }
            return responsedoc;
        }


        public async Task<List<OpenLibraryDoc>?> MostReadBookAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=fantasy&has_fulltext=true&ebook_access=public");
            //var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/fantasy.json");
            return response!.Docs!.Take(10).ToList() ?? new List<OpenLibraryDoc>();
        }



       

        public async Task<List<OpenLibraryDoc>> AdventureAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/adventure.json");
            return response!.Docs!.Take(10).ToList() ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryModel>> RomanceAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/romance.json");
            return response!.Works!.Take(10).ToList() ?? new List<OpenLibraryModel>();
        }

        public async Task<List<OpenLibraryDoc>> ScienceAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/science.json");
            return response!.Docs!.Take(10).ToList() ?? new List<OpenLibraryDoc>();
        }


        public async Task<List<OpenLibraryDoc>> MysteryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/mystery.json");
            return response!.Docs!.Take(10).ToList() ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> ChildrenAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/children.json");
            return response!.Docs!.Take(10).ToList() ?? new List<OpenLibraryDoc>();
        }


        public async Task<List<OpenLibraryDoc>> PoetryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/poetry.json");
            return response!.Docs!.Take(10).ToList() ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> HistoryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/history.json");
            return response!.Docs!.Take(10).ToList() ?? new List<OpenLibraryDoc>();
        }

        public async Task<List<OpenLibraryDoc>> ShortStoriesAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/short_stories.json");
            return response!.Docs!.Take(10).ToList() ?? new List<OpenLibraryDoc>();
        }


        public async Task<List<OpenLibraryDoc>> ClassicsAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/subjects/classic_literature.json");
            return response!.Docs!.Take(10).ToList() ?? new List<OpenLibraryDoc>();
        }








        /*   foreach (var a in responsework!.Authors!)
                 {
                     var author = await _http.GetFromJsonAsync<AuthorNames>($"https://openlibrary.org{a.author!.Key}.json");

                     Console.WriteLine($"name{author!.Name}");
                     responsework.AuthorName = author.Name;
                 }*/

        /*      responsework.Publisher = editionsResponse.Entries.Where(s => s.Publisher != null).SelectMany(s => s.Publisher!).ToList();
              responsework.ISBN = editionsResponse.Entries.Where(s => s.ISBN != null).SelectMany(s => s.ISBN!).Take(1).ToList();
              responsework.PubLishedDate = editionsResponse.Entries.Select(s => s.PublishedDate).FirstOrDefault( s => !string.IsNullOrEmpty(s));
              responsework.SubTitle = editionsResponse.Entries.Select(s => s.SubTitle).FirstOrDefault(s => !string.IsNullOrEmpty(s));
              responsework.Series = editionsResponse.Entries.Where(s => s.Series != null).SelectMany(s => s.Series!).ToList();
              responsework.PublishedPlace = editionsResponse.Entries.Where(s => s.PublishedPlace != null).SelectMany(s => s.PublishedPlace!).ToList();
              responsework.OCAID = editionsResponse.Entries.Select(s => s.OCAID).FirstOrDefault(s => !string.IsNullOrEmpty(s));
         */
    }
}
