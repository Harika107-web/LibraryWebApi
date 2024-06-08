using LibraryWebApi.Services.BackupStore;
using LibraryWebApi.Services.BookStore;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [ApiVersion("2.0")]
  [ApiExplorerSettings(GroupName = "v2")]
  public class BookStoreController2 : ControllerBase
  {
    private readonly IBackupStoreService _backupStoreService;
private readonly ILogger<BookStoreController2> _logger;
        public BookStoreController2(IBackupStoreService backupStoreService, ILogger<BookStoreController2> logger)
        {
            _backupStoreService = backupStoreService;
            _logger = logger;
        }

        [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
            _logger.LogInformation("Doing some work.");
            try
      {
        return Ok(await _backupStoreService.GetBooks());
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
  }
}