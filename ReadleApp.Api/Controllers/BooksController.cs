using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadleApp.Domain.Model;
using ReadleApp.Infrastructure.Services;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ReadleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookApiServices? _bookApi;
        public BooksController(BookApiServices? bookApi)
        {
            _bookApi = bookApi;
        }
        [HttpGet("MostRead")]
        public async Task<IActionResult> MostRead()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.MostReadBookAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }

        [HttpGet("Adventure")]
        public async Task<IActionResult> Adventure()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.AdventureAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }

        [HttpGet("Romance")]
        public async Task<IActionResult> RomanceAsync()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.RomanceAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }

        [HttpGet("Science")]
        public async Task<IActionResult> ScienceAsync()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.ScienceAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }

        [HttpGet("Mystery")]
        public async Task<IActionResult> MysteryAsync()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.MysteryAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }


        [HttpGet("Children")]
        public async Task<IActionResult> ChildrenAsync()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.ChildrenAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }


        [HttpGet("Poetry")]
        public async Task<IActionResult> PoetryAsync()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.PoetryAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }


        [HttpGet("History")]
        public async Task<IActionResult> HistoryAsync()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.HistoryAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }

        [HttpGet("ShortStories")]
        public async Task<IActionResult> ShortStoriesAsync()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.ShortStoriesAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }

        [HttpGet("Classics")]
        public async Task<IActionResult> ClassicsAsync()
        {
            var books = new List<OpenLibraryPreviewDetails>();

            await foreach (var book in _bookApi!.ClassicsAsync()!)
            {
                books.Add(book);
            }

            if (books.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }

            return Ok(books);
        }

      
        [HttpGet("Fulltext/{fulltext}")]
        public async Task<IActionResult> GetFullText(string fulltext)
        {
            string response = await _bookApi!.GetFullText(fulltext);
            if(response is null)
            {
                return BadRequest(new { Message = " No Ia on this Book" });
            }
            return Ok(response);
        }
        [HttpGet("GetDetails/{workkey}")]
        public async Task<IActionResult> GetDetails(string workkey)
        {
            var response = await _bookApi!.GetDetails(workkey);
            if(response is null)
            {
                return BadRequest(new { Message = "Something went wrong" });
            }
            return Ok(response);
        }
        [HttpGet("Cover/{cover}")]
        public async Task<IActionResult> GetCover(int cover)
        {
            var http = new HttpClient();
          var url = $"https://covers.openlibrary.org/b/id/{cover}-L.jpg";
           var bytes = await http.GetByteArrayAsync(url);
            var base64 = Convert.ToBase64String(bytes);
            return Ok(base64);
        }
        [HttpGet("ViewBook/{workkey}")]
        public async Task<IActionResult> ViewBook(string workkey)
        {
            var response = await _bookApi!.ViewBook(workkey);
            if(response is null)
            {
                return BadRequest(new { Messsage = "No Details"});
            }
            return Ok(response);
        }
        [HttpGet("BookShelves/{workkey}")]
        public async Task<IActionResult> GetBookShelves(string workkey)
        {
            var responsse = await _bookApi!.BookShelvesAsync(workkey);
            if(responsse is null)
            {
                return BadRequest(new { Message = "No Details" });
            }
            return Ok(responsse);
        }
        [HttpGet("Editions/{workkey}")]
        public async Task<IActionResult> GetEdition(string workkey)
        {
            var response = await _bookApi!.GetEditionAsync(workkey);
            if(response is null)
            {
                return BadRequest(new { Message = "No Details" });
            }
            return Ok(response);
        }
    }
}
