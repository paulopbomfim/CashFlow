using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterExpenseResponse), StatusCodes.Status201Created)]
    public IActionResult Register([FromBody] RegisterExpenseRequest request)
    {
        var response = new RegisterExpenseUseCase().Execute(request);

        return Created(string.Empty, response);
    }
}
