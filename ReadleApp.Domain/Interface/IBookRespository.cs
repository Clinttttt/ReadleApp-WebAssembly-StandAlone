using ReadleApp.Domain;
using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReadleApp.Domain.Model.OpenLibraryModel;

namespace ReadleApp.Domain.Interface
{
    public interface IBookRespository
    {
        Task SaveBookAsync(OfflineReadingModel book);
        Task<List<OpenLibraryDoc>> GetAllBooks();
        Task<List<OpenLibraryDoc>> GetMostReadAsync(string Category);
        Task<OpenLibraryDoc> GetBookById(string Bookid);
        //Task<bool> HasBookEachTopicAsync(string topic);
        Task SaveTenBookAsync(List<OfflineReadingModel> book);
        Task<List<OfflineReadingModel>> GetTenBookAsync(string? category = null);
        Task<bool> HasAnyBooksAsync();
       
    }
}