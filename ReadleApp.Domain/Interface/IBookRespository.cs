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
        Task SaveBookAsync(BookGutendex book);
        Task<List<BookGutendex>> GetAllBooks();
        Task<List<BookGutendex>> GetMostReadAsync(string Category);
        Task<BookGutendex?> GetBookById(BookGutendex id);
        Task<bool> HasBookEachTopicAsync(string topic);
        Task SaveTenBookAsync(List<BookGutendex> book);
        Task<List<BookGutendex>> GetTenBookAsync(string? category = null);
        Task<bool> HasAnyBooksAsync();
    }
}
