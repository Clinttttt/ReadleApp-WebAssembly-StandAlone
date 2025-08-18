using System.Security.Cryptography.X509Certificates;
using TG.Blazor.IndexedDB;

namespace ReadleApp.Client
{
    public class IndexDb
    {
        private readonly IndexedDBManager _db;
        public IndexDb(IndexedDBManager db)
        {
            _db = db;
        }
        public async Task SaveBookAsync( BookGutendex book)
        {
            var Results = new StoreRecord<BookGutendex>
            {
                Storename = "Books",
                Data = book

            };
            await _db.AddRecord(Results);
        }
        public async Task SaveTenBooks(List<BookGutendex> book)
        {
            foreach (var books in book)
            {
                await SaveBookAsync(books);
            }
        }
        public async Task<List<BookGutendex>> GetTenBooks()
        {
            var Results = await _db.GetRecords<BookGutendex>("Books");
            return Results.Take(10).ToList() ?? new List<BookGutendex>();
        }
        public async Task<bool> HasBook()
        {
            var results = await  _db.GetRecords<BookGutendex>("Books");
            return results.Count > 0;
        }

    }
}
