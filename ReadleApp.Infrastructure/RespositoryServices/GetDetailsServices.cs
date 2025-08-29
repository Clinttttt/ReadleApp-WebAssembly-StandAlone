using ReadleApp.Domain.Interface;
using ReadleApp.Domain.Model;
using ReadleApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadleApp.Infrastructure.RespositoryServices
{
    public class GetDetailsServices : IGetDetailsServices
    {
        private HttpClient _http;
        private readonly BookServerServices _services;
        public GetDetailsServices(HttpClient http, BookServerServices services)
        {
            _http = http;
            _services = services;
        }

        public async Task<OpenLibraryViewDetails> GetBookyAsync(string workkey)
        {
            var responsework = await _services.GetWorkDetails(workkey);
            var responsedoc = await _services.GetDocDetails(workkey);
            var responseshelves = await _services.BookShelvesAsync(workkey);
            var responseedition = await _services.GetEditionAsync(workkey);

            if (responsedoc is null)
                return new OpenLibraryViewDetails();

            var viewdetails = new OpenLibraryViewDetails
            {
                Workkey = workkey,
                Title = responsedoc!.Title,
                Authorname = responsedoc.AuthorName,
                Coverkey = responsedoc.CoverKey,
                SubTitle = responseedition?.SubTitle,
                Publishdate = responseedition?.PublishedDate,
                Description = responsework?.DescriptionRaw,
                Subjects = responsework?.Subject,
                Publishers = responseedition?.Publisher,
                Bookshelves = responseshelves?.GetBookshelves,
            };
            return viewdetails;
        }
    }
}


