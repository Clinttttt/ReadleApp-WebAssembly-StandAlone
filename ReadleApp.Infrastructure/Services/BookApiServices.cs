using ReadleApp.Domain;
using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
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


       
        public async Task<OpenLibraryEntities?> GetBookAsync(string workkey)

        {
            var responsework = await _http.GetFromJsonAsync<OpenLibraryModel>($"https://openlibrary.org/works/{workkey}.json");
            var editionsResponse = await _http.GetFromJsonAsync<OpenEditionResponse>($"https://openlibrary.org/works/{workkey}/editions.json");
            var bookshelvesResponse = await _http.GetFromJsonAsync<OpenLibraryBookShelves>($"https://openlibrary.org/works/{workkey}/bookshelves.json");
            var responsedoc = await _http.GetFromJsonAsync<OpenLibraryResponse>($"https://openlibrary.org/search.json?q={workkey}");
            var firstDoc = responsedoc!.Docs!.FirstOrDefault( s => s.WorkKey == $"/works/{workkey}");
            var _LibraryEntities = new OpenLibraryEntities();
            if (firstDoc is not null)
            {                      
            var GetAi = firstDoc!.IA!.FirstOrDefault();
            try
            {


                var FetchIa = await _http.GetStringAsync($"https://archive.org/stream/{GetAi}/{GetAi}_djvu.txt");
                
                    _LibraryEntities.FullText = FetchIa;
               
                //Console.Write($"FullText{_LibraryEntities.FullText}");

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

                        bookshelvesResponse!.WorkKey = firstDoc!.WorkKey;
                        _LibraryEntities.GetBookshelve = bookshelvesResponse.GetBookshelves;
                        _LibraryEntities.PublishedDate = editionsResponse!.Entries.Select(s => s.PublishedDate).FirstOrDefault(s => !string.IsNullOrEmpty(s));
                        _LibraryEntities.Publishers = editionsResponse.Entries.Where(s => s.Publisher != null).SelectMany(s => s.Publisher!).Distinct().ToList();
                        _LibraryEntities.Subject = responsework!.Subject;
                        _LibraryEntities.SubTitle = editionsResponse.Entries.Select(s => s.SubTitle).FirstOrDefault(s => !string.IsNullOrEmpty(s));

                        if (responsedoc is not null)
                        {

                            _LibraryEntities.Workkey = firstDoc.WorkKey;
                            firstDoc.DescriptionClones = responsework.DescriptionRaw;
                            _LibraryEntities.Description = firstDoc.DescriptionClones;
                            _LibraryEntities.Title = firstDoc.Title;
                            _LibraryEntities.AuthorName = firstDoc.AuthorName;
                            _LibraryEntities.CoverKey = firstDoc.CoverKey;
                          
                            _LibraryEntities.Languages = firstDoc.Languages;
                            _LibraryEntities.IA = firstDoc.IA!.FirstOrDefault();


                        }
                    }

                }
            }
            return _LibraryEntities;
        }

        public async Task<string?> CoverConvertBase64(int? CoverKey)
        {         
            if (!CoverKey.HasValue) return null;
            var fetch = $"https://covers.openlibrary.org/b/id/{CoverKey}-L.jpg";
            var convert = await _http.GetByteArrayAsync(fetch);
            return Convert.ToBase64String(convert);
        }
        public async Task<List<OfflineReadingModel>?> MostReadBookAsync()
        { 
        
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=fantasy&has_fulltext=true&ebook_access=public");        
            var OfflineModel = new List<OfflineReadingModel>();
    
            if (response is not null)
                foreach (var details in response!.Docs!.Take(20))
            {
                {
                    string? description = null;
                    var responsework = await _http.GetFromJsonAsync<OpenLibraryModel>($"https://openlibrary.org/works/{details.WorkKey}.json");
                    if(responsework is not null)
                    {
                        details.DescriptionClones = responsework.DescriptionRaw;
                        if (details.DescriptionClones is JsonElement element)
                        { 
                            if (element.ValueKind == JsonValueKind.String) description = element.GetString();
                            if (element.ValueKind == JsonValueKind.Object && element.TryGetProperty("value", out var val))
                                description = val.GetString();
                        }
                    }
                    var fetchIa = details.IA!.FirstOrDefault();
                    if(fetchIa is not null)
                    {
                        var FetchIa = await _http.GetStringAsync($"https://archive.org/stream/{fetchIa}/{fetchIa}_djvu.txt");
                        details.FullText = FetchIa;
                    }
                    var ia = details.IA!.FirstOrDefault();
                    var detail = new OfflineReadingModel
                    {
                        _Workkey = details.WorkKey,
                        _IA = ia,
                        _Title = details.Title,
                        _AuthorName = details.AuthorName,
                        _Description = description,
                        _FullText = details.FullText,
                        _CoverBase64 = await CoverConvertBase64(details.CoverKey),
                        
                    };

                    OfflineModel.Add(detail);
                }

            }
            
            return OfflineModel;
            
        }
      

        public async Task<List<OpenLibraryDoc>> AdventureAsync()  
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=adventure&has_fulltext=true&ebook_access=public");
            return response!.Docs!.Take(20).ToList() ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> RomanceAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=romance&has_fulltext=true&ebook_access=public");
            return response!.Docs!.Take(20).ToList() ?? new List<OpenLibraryDoc>();
        }

        public async Task<List<OpenLibraryDoc>> ScienceAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=science&has_fulltext=true&ebook_access=public");
            return response!.Docs!.Take(20).ToList() ?? new List<OpenLibraryDoc>();
        }


        public async Task<List<OpenLibraryDoc>> MysteryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=mystery&has_fulltext=true&ebook_access=public");
            return response!.Docs!.Take(20).ToList() ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> ChildrenAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=children&has_fulltext=true&ebook_access=public");
            return response!.Docs!.Take(20).ToList() ?? new List<OpenLibraryDoc>();
        }


        public async Task<List<OpenLibraryDoc>> PoetryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=poetry&has_fulltext=true&ebook_access=public");
            return response!.Docs!.Take(20).ToList() ?? new List<OpenLibraryDoc>();
        }
        public async Task<List<OpenLibraryDoc>> HistoryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=history&has_fulltext=true&ebook_access=public");
            return response!.Docs!.Take(20).ToList() ?? new List<OpenLibraryDoc>();
        }

        public async Task<List<OpenLibraryDoc>> ShortStoriesAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=short_stories&has_fulltext=true&ebook_access=public");
            return response!.Docs!.Take(20).ToList() ?? new List<OpenLibraryDoc>();
        }


        public async Task<List<OpenLibraryDoc>> ClassicsAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=classic_literature&has_fulltext=true&ebook_access=public");
            return response!.Docs!.Take(20).ToList() ?? new List<OpenLibraryDoc>();
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
