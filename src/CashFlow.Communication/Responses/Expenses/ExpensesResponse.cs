namespace CashFlow.Communication.Responses;

public class ExpensesResponse
{
    public IList<ShortExpenseResponse> Expenses { get; set; } = [];
}