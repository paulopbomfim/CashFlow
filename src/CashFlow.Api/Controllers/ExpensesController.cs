using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterExpensesResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterExpenseUseCase useCase, [FromBody] ExpenseRequest request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ExpensesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllExpenseUseCase useCase)
    {
        var response = await useCase.Execute();
        
        if (response.Expenses.Any())
            return Ok(response);

        return NoContent();
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ExpenseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromServices] IGetByIdExpenseUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);
        
        return Ok(response);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteExpenseUseCase useCase, [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateExpenseUseCase useCase,
        [FromRoute] long id,
        [FromBody] ExpenseRequest request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }
}
