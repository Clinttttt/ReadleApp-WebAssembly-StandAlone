using ReadleApp.Domain;
using ReadleApp.Domain.Interface;
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
        private readonly IMapToOffline _toOffline;
        private readonly IPreviewDetails _preview;

        public BookApiServices(HttpClient http, IMapToOffline toOffline, IPreviewDetails preview)
        {
            _http = http;
            _toOffline = toOffline;
            _preview = preview;


        }



        public async Task<OpenLibraryEntities?> GetBookAsync(string workkey)

        {
            var responsework = await _http.GetFromJsonAsync<OpenLibraryModel>($"https://openlibrary.org/works/{workkey}.json");
            var editionsResponse = await _http.GetFromJsonAsync<OpenEditionResponse>($"https://openlibrary.org/works/{workkey}/editions.json");
            var bookshelvesResponse = await _http.GetFromJsonAsync<OpenLibraryBookShelves>($"https://openlibrary.org/works/{workkey}/bookshelves.json");
            var responsedoc = await _http.GetFromJsonAsync<OpenLibraryResponse>($"https://openlibrary.org/search.json?q={workkey}");
            var firstDoc = responsedoc!.Docs!.FirstOrDefault(s => s.WorkKey == $"/works/{workkey}");
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
        public async Task<string> GetFullText(string FirstIa)
        {
           return  await _http.GetStringAsync($"https://archive.org/stream/{FirstIa}/{FirstIa}_djvu.txt");
        }
        public async Task<OpenLibraryModel?> GetDetails(string workstring)
        {
           return  await _http.GetFromJsonAsync<OpenLibraryModel>($"https://openlibrary.org/works/{workstring}.json");
        }
   
        public async Task<List<OpenLibraryPreviewDetails>?> MostReadBookAsync()
        {

            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=fantasy&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OpenLibraryPreviewDetails>();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }
        public async Task<List<OpenLibraryPreviewDetails>> AdventureAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=adventure&has_fulltext=true&ebook_access=public");
          var OfflineModel = new List<OpenLibraryPreviewDetails>();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }
        public async Task<List<OpenLibraryPreviewDetails>> RomanceAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=romance&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OpenLibraryPreviewDetails>();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }
        public async Task<List<OpenLibraryPreviewDetails>> ScienceAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=science&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OpenLibraryPreviewDetails   >();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }

        public async Task<List<OpenLibraryPreviewDetails>> MysteryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=mystery&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OpenLibraryPreviewDetails   >();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }

        public async Task<List<OpenLibraryPreviewDetails>> ChildrenAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=children&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OpenLibraryPreviewDetails>();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }
        public async Task<List<OpenLibraryPreviewDetails>> PoetryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=poetry&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OpenLibraryPreviewDetails>();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }

        public async Task<List<OpenLibraryPreviewDetails>> HistoryAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=history&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OpenLibraryPreviewDetails>();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }
        public async Task<List<OpenLibraryPreviewDetails>> ShortStoriesAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=short_stories&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OpenLibraryPreviewDetails>();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

        }

        public async Task<List<OpenLibraryPreviewDetails>> ClassicsAsync()
        {
            var response = await _http.GetFromJsonAsync<OpenLibraryResponse>("https://openlibrary.org/search.json?subject=classic_literature&has_fulltext=true&ebook_access=public");
            var OfflineModel = new List<OpenLibraryPreviewDetails>();

            if (response is not null)
                foreach (var details in response!.Docs!.Take(10))

                {
                    var data = await _preview.PreviewAsync(details);
                    OfflineModel.Add(data);
                }

            return OfflineModel;

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
