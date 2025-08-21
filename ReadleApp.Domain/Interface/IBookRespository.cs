using ReadleApp.Domain;
using ReadleApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadleApp.Domain.Interface
{
    public interface IBookRespository
    {
        Task SaveBookAsync(OpenLibraryModel book);
        Task<List<OpenLibraryModel>> GetAllBooks();
        Task<List<OpenLibraryModel>> GetMostReadAsync(string Category);
        Task<OpenLibraryModel?> GetBookById(int Bookid);
        Task<bool> HasBookEachTopicAsync(string topic);
        Task SaveTenBookAsync(List<OpenLibraryModel> book);
        Task<List<OpenLibraryModel>> GetTenBookAsync(string? category = null);
        Task<bool> HasAnyBooksAsync();
    }
}
