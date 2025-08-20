
using ReadleApp.Domain.Interface;
using ReadleApp.Domain.Model;

namespace ReadleApp.Client.Services
{
    public class BookStateService
    {
        public event Action? OnChange;
        public Dictionary<string, List<BookGutendex>> AllBook { get; set; } = new();
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

            var Categories = new List<string> { "MostRead", "Adventure", "Romance", "Science", "Mystery", "Children", "Poetry", "History", "ShortStories", "Classics" };
            foreach (var category in Categories)
            {
                var books = await _db.GetTenBookAsync(category);
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
                        "ShortStories" => await  _bookClient.ShortStoriesAsync(),
                        "Classics" => await _bookClient.ClassicsAsync(),
                       _ => new List<BookGutendex>()
                    };
                       books = books.GroupBy(s => s.Id).Select(s => s.First()).Take(10).ToList();
                   foreach(var book in books)
                    {
                        book.Category = category;
                        await _db.SaveBookAsync(book);
                    }
                  

                }
                AllBook[category] = books;

            }
            IsInitialized = true;
            NotifyStateChanged();

        }

    }
}
