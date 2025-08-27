using ReadleApp.Domain;
using ReadleApp.Domain.Interface;
using ReadleApp.Domain.Model;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using static ReadleApp.Domain.Model.OpenLibraryModel;
using static System.Net.WebRequestMethods;

namespace ReadleApp.Client.Services
{
    public class BookStateService
    {
        public event Action? OnChange;
        public Dictionary<string, List<OfflineReadingModel>> AllBook { get; set; } = new();
        public bool IsInitialized { get; set; } = false;
        private readonly IBookRespository _db;
        private readonly BookClientServices _bookClient;

        public BookStateService(IBookRespository db, BookClientServices bookClient)
        {
            _db = db;
            _bookClient = bookClient;

        }
        private void NotifyStateChanged() => OnChange?.Invoke();

        public async Task InitializeAsync()
        {
            if (IsInitialized) return;


            //await LoadCategoryAsync("MostRead");
            //NotifyStateChanged();

            var categories = new List<string>
                {
                   "MostRead", "Adventure", "Romance", "Science", "Mystery", 
                    "Children", "Poetry", "History", "ShortStories", "Classics"
                };

            foreach (var category in categories)
            {
                await LoadCategoryAsync(category);
                NotifyStateChanged();
            }

            IsInitialized = true;

        }

        private async Task LoadCategoryAsync(string category)
        {
            var books = await _db.GetTenBookAsync(category) ?? new List<OfflineReadingModel>();

            if (books == null || books.Count == 0)
            {
                books = category switch
                {
                    "MostRead" => await _bookClient.MostReadAsync(),
                    "Adventure" => await _bookClient.AdventureAsync(),
                    "Romance" => await _bookClient.RomanceAsync(),
                    "Science" => await _bookClient.ScienceAsync(),
                    "Mystery" => await _bookClient.MysteryAsync(),
                    "Children" => await _bookClient.ChildrenAsync(),
                    "Poetry" => await _bookClient.PoetryAsync(),
                    "History" => await _bookClient.HistoryAsync(),
                    "ShortStories" => await _bookClient.ShortStoriesAsync(),
                    "Classics" => await _bookClient.ClassicsAsync(),
                    _ => new List<OfflineReadingModel>()
                };


                books = books.GroupBy(s => s._Workkey)
                             .Select(s => s.First())
                             .Take(20)
                             .ToList();

                AllBook[category] = new List<OfflineReadingModel>();



                foreach (var book in books.Take(20))
                {
                    book.Category = category;

                    await _db.SaveBookAsync(book);

                    //AllBook[category].Add(book);

                    NotifyStateChanged();
                }
            }

            AllBook[category] = books;
        
        }


       

    }
}

















/* public async Task<OpenLibraryModel?> GetBookByIsbnAsync(string isbn)
        {
            using var http = new HttpClient();
            var url = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&format=json&jscmd=data";

            var response = await http.GetFromJsonAsync<Dictionary<string, OpenLibraryModel>>(url);

            return response?[$"ISBN:{isbn}"];
        }*/