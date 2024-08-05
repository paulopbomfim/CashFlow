using CashFlow.Communication.Requests.Login;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Login;

public class DoLoginUseCase: IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    
    public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
        _accessTokenGenerator = accessTokenGenerator;
    }
    
    public async Task<RegisteredUserResponse> Execute(LoginRequest request)
    {
        var user = await _repository.GetUserByEmail(request.Email) ?? throw new InvalidLoginException();
        
        var passwordMatch = _passwordEncrypter.Verify(request.Password, user.Password);
        
        if(!passwordMatch) throw new InvalidLoginException();
        
        return new RegisteredUserResponse
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }
}