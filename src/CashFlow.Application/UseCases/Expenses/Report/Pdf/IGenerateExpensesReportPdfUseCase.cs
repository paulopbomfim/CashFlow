namespace CashFlow.Application.UseCases.Expenses;

public interface IGenerateExpensesReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly date);
}