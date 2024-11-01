using System.Net.Mime;
using CashFlow.Application.UseCases.Expenses;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateExpensesReportExcelUseCase useCase,
        [FromHeader] DateOnly date)
    {
        var file = await useCase.Execute(date);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();
    }
    
    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Pdf(
        [FromServices] IGenerateExpensesReportPdfUseCase useCase,
        [FromHeader] DateOnly date)
    {
        var file = await useCase.Execute(date);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }
}

