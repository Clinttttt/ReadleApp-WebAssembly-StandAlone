using Microsoft.Extensions.Logging.Abstractions;
using ReadleApp.Domain;
using ReadleApp.Domain.Interface;
using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Blazor.IndexedDB;
using static ReadleApp.Domain.Model.OpenLibraryModel;

namespace ReadleApp.Infrastructure.Services.IndexDb
{
    public class BookRespository : IBookRespository
    {
        private readonly IndexedDBManager _db;

        public BookRespository(IndexedDBManager db)
        {
            _db = db;
        }
       
        public async Task SaveBookAsync(OfflineReadingModel book)
        {

            var record = new StoreRecord<OfflineReadingModel>
            {
                Storename = "Books",
                Data = book
            };

            await _db.AddRecord(record);
        }

        public async Task SaveTenBookAsync(List<OfflineReadingModel> books)
        {
            foreach (var book in books.Take(10))
            {   
                await SaveBookAsync(book);
            }
        }
        public async Task<List<OfflineReadingModel>> GetTenBookAsync(string? category)
        {
            var Results = await _db.GetRecords<OfflineReadingModel>("Books") ?? new List<OfflineReadingModel>();

            return Results.Where(b => !string.IsNullOrEmpty(b.Category) && b.Category!.Equals(category, StringComparison.OrdinalIgnoreCase)).Take(10).ToList();


        }

        public async Task<List<OpenLibraryDoc>> GetMostReadAsync(string Category)
        {
            var Results = await _db.GetRecords<OpenLibraryDoc>("Books");
            return Results.Where(s => s.Category!.Equals(Category)).ToList();
        }
     



        public async Task<List<OpenLibraryDoc>> GetAllBooks()
        {
            var result = await _db.GetRecords<OpenLibraryDoc>("Books");
            return result.ToList();
        }

        public async Task<OpenLibraryDoc?> GetBookById(string? workkey)
        {
           
            if (string.IsNullOrEmpty(workkey))
            {
                return null;
            }

            var Results = await _db.GetRecords<OpenLibraryDoc>("Books");

            return Results.FirstOrDefault(s => s.WorkKey == workkey);

        }

        public async Task DeleteBook(int id)
        {
            await _db.DeleteRecord("Books", id);
        }
        /*   public async Task<bool> HasBookEachTopicAsync(string topic)
           {
               var books = await _db.GetRecords<OpenLibraryDoc>("Books");
               return books.Any(b => b.Subjects!.Any(s => s.Contains(topic, StringComparison.OrdinalIgnoreCase)));
           }*/
        public async Task<bool> HasAnyBooksAsync()
        {
            var books = await _db.GetRecords<OpenLibraryDoc>("Books");
            return books.Count > 0;
        }
    }
}
