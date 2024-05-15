using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses;

public interface IGetAllExpenseUseCase
{
    Task<ExpensesResponse> Execute();
}