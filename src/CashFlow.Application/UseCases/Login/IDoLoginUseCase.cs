using CashFlow.Communication.Requests.Login;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Login;

public interface IDoLoginUseCase
{
    Task<RegisteredUserResponse> Execute(LoginRequest request);
}