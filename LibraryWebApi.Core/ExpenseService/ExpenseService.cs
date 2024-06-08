using LibraryWebApi.Common.Models;

namespace LibraryWebApi.Core.ExpenseService
{
  public class ExpenseService : IExpenseService
  {
    const int MINIMIM_EXPENSES = 500;
    private readonly List<Expense> _dummyExpenses = new List<Expense>();

    public ExpenseService()
    {
      _dummyExpenses.Add(new Expense() { Month = "Jan", Amount = 2300 });
      _dummyExpenses.Add(new Expense() { Month = "Feb", Amount = 6750 });
      _dummyExpenses.Add(new Expense() { Month = "Mar", Amount = 890 });
    }

    public async Task<Expense> AddMonthExpense(string month, int amount)
    {
      if (string.IsNullOrEmpty(month))
      {
        throw new ArgumentNullException("month");
      }

      if (amount < 0)
      {
        throw new ArgumentException("amount can not be less than zero");
      }

      if (amount == 0) // Add mininum expenses
      {
        amount = MINIMIM_EXPENSES;
      }

      _dummyExpenses.Add(new Expense { Month = month, Amount = amount });

      return await Task.Run(() => _dummyExpenses.Last());
    }

    public async Task<int> GetTotalExpense()
    {
      return await Task.Run(() => _dummyExpenses.Select(x => x.Amount).Sum());
    }
  }
}
