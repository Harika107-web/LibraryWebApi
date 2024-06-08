using LibraryWebApi.Core.UserService;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(string name)
    {
      var result = await _userService.GetUser(name);
      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(string name, int age)
    {
      var result = await _userService.AddUser(name, age);
      return Ok(result);
    }
  }
}
