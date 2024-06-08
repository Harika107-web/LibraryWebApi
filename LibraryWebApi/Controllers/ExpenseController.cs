using LibraryWebApi.Core.ExpenseService;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExpenseController : ControllerBase
  {
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
      _expenseService = expenseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddMonthExpense(string month, int amount)
    {
      var result = await _expenseService.AddMonthExpense(month, amount);
      return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTotatlExpense()
    {
      var result = await _expenseService.GetTotalExpense();
      return Ok(result);
    }
  }
}
