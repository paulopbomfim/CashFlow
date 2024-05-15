using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses;

public interface IGetByIdExpenseUseCase
{
    Task<ExpenseResponse> Execute(long id);
}