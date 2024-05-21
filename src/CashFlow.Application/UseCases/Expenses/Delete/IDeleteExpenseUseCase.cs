namespace CashFlow.Application.UseCases.Expenses;

public interface IDeleteExpenseUseCase
{
    Task Execute(long id);
}