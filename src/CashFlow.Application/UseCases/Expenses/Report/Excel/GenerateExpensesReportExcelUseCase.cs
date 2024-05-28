using CashFlow.Domain.Reports;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(DateOnly date)
    {
        var workbook = new XLWorkbook();

        workbook.Author = "Paulo Bomfim";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Calibri";

        var worksheet = workbook.Worksheets.Add($"Expenses-{date.ToString("Y")}");
        
        InsertHeader(worksheet);

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ReportGenerationMessagesResource.TITLE;
        worksheet.Cell("B1").Value = ReportGenerationMessagesResource.DATE;
        worksheet.Cell("C1").Value = ReportGenerationMessagesResource.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ReportGenerationMessagesResource.AMOUNT;
        worksheet.Cell("E1").Value = ReportGenerationMessagesResource.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#006769");
        
        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}