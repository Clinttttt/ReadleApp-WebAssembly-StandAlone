using ReadleApp.Domain;
using ReadleApp.Domain.Interface;
using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Blazor.IndexedDB;

namespace ReadleApp.Infrastructure.Services.IndexDb
{
    public class BookRespository : IBookRespository
    {
        private readonly IndexedDBManager _db;

        public BookRespository(IndexedDBManager db)
        {
            _db = db;
        }
       
        public async Task SaveBookAsync(BookGutendex book)
        {

            var record = new StoreRecord<BookGutendex>
            {
                Storename = "Books",
                Data = book
            };

            await _db.AddRecord(record);
        }

        public async Task SaveTenBookAsync(List<BookGutendex> books)
        {
            foreach (var book in books)
            {
                await SaveBookAsync(book);
            }
        }


        public async Task<List<BookGutendex>> GetMostReadAsync(string Category)
        {
            var Results = await _db.GetRecords<BookGutendex>("Books");
            return Results.Where(s => s.Category!.Equals(Category)).ToList();
        }
        public async Task<List<BookGutendex>> GetTenBookAsync(string? category)
        {
            var Results = await _db.GetRecords<BookGutendex>("Books");
            
               return Results.Where(b => b.Category!.Equals(category, StringComparison.OrdinalIgnoreCase)).Take(10).ToList();
            
 
        }



        public async Task<List<BookGutendex>> GetAllBooks()
        {
            var result = await _db.GetRecords<BookGutendex>("Books");
            return result.ToList();
        }

        public async Task<BookGutendex?> GetBookById(BookGutendex id)
        {
            return await _db.GetRecordById<BookGutendex, BookGutendex>("Books", id);
        }

        public async Task DeleteBook(int id)
        {
            await _db.DeleteRecord("Books", id);
        }
        public async Task<bool> HasBookEachTopicAsync(string topic)
        {
            var books = await _db.GetRecords<BookGutendex>("Books");
            return books.Any(b => b.Subjects!.Any(s => s.Contains(topic, StringComparison.OrdinalIgnoreCase)));
        }
        public async Task<bool> HasAnyBooksAsync()
        {
            var books = await _db.GetRecords<BookGutendex>("Books");
            return books.Count > 0;
        }
    }
}
