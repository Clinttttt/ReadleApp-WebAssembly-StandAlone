using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadleApp.Infrastructure.Services;
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
            var response = await _bookApi!.MostReadBookAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }
        [HttpGet("Adventure")]
        public async Task<IActionResult> Adventure()
        {
            var response = await _bookApi!.AdventureAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }

        [HttpGet("Romance")]
        public async Task<IActionResult> RomanceAsync()
        {
            var response = await _bookApi!.RomanceAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }
        [HttpGet("Science")]
        public async Task<IActionResult> ScienceAsync()
        {
            var response = await _bookApi!.ScienceAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }
        [HttpGet("Mystery")]
        public async Task<IActionResult> MysteryAsync()
        {
            var response = await _bookApi!.MysteryAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }
        [HttpGet("Children")]
        public async Task<IActionResult> ChildrenAsync()
        {
            var response = await _bookApi!.ChildrenAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }
        [HttpGet("Poetry")]
        public async Task<IActionResult> PoetryAsync()
        {
            var response = await _bookApi!.PoetryAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }
        [HttpGet("History")]
        public async Task<IActionResult> HistoryAsync()
        {
            var response = await _bookApi!.HistoryAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }
        [HttpGet("ShortStories")]
        public async Task<IActionResult> ShortStoriesAsync()
        {
            var response = await _bookApi!.ShortStoriesAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }
        [HttpGet("Classics")]
        public async Task<IActionResult> ClassicsAsync()
        {
            var response = await _bookApi!.ClassicsAsync();
            if (response == null || response.Count == 0)
            {
                return BadRequest(new { message = "No Book" });
            }
            return Ok(response.Take(10).ToList());
        }
        [HttpGet("GetBookById")]
        public async Task<ActionResult> GetBookAsync(string Id)
        {
            var response = await _bookApi!.GetBookAsync(Id);
            if(response == null)
            {
                return BadRequest(new { Message = "No Book" });
            }
            return Ok(response);
        }



    }
}
