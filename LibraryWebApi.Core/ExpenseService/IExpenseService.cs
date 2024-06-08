using LibraryWebApi.Common.Models;

namespace LibraryWebApi.Core.ExpenseService
{
  public interface IExpenseService
  {
    Task<Expense> AddMonthExpense(string month, int amount);
    Task<int> GetTotalExpense();
  }
}
