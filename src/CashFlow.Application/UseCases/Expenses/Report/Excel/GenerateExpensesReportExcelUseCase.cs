using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    public Task<byte[]> Execute(DateOnly date)
    {
        var workbook = new XLWorkbook();

        workbook.Author = "Paulo Bomfim";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Calibri";

        var worksheet = workbook.Worksheets.Add($"Expenses-{date.ToString("Y")}");
        
        
    }
}