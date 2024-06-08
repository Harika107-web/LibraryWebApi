using LibraryWebApi.Core.BookService;
using LibraryWebApi.Services.BookStore;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BookStoreController1 : ControllerBase
    {
        private readonly IBookService _bookStoreService;
        private readonly ILogger<BookStoreController1> _logger;

        public BookStoreController1(IBookService bookStoreService, ILogger<BookStoreController1> logger)
        {
            _bookStoreService = bookStoreService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(int Id)
        {
            _logger.LogInformation("Fetch Books for Id: {0}",Id);

            try
            {
                _logger.LogInformation("Sucesfully fetch Books");

                return Ok(await _bookStoreService.GetBookByIdAsync(Id));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to fetch Books {0}", ex);

                return StatusCode(500);
            }
        }
    }
}
