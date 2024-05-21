using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, ExpenseRequest request);
}