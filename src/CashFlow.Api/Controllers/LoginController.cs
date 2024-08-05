using CashFlow.Application.UseCases.Login;
using CashFlow.Communication.Requests.Login;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Login(
            [FromServices] IDoLoginUseCase useCase,
            [FromBody] LoginRequest request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
