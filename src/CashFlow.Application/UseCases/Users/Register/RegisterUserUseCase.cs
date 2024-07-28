using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Users;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    public RegisterUserUseCase(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<RegisteredUserResponse> Execute(RegisterUserRequest request)
    {
        Validate(request);
        
        var user = _mapper.Map<User>(request);
        
        return new RegisteredUserResponse
        {
            Name = user.Name,
        };
    }

    private static void Validate(RegisterUserRequest request)
    {
        var result = new RegisterUserValidator().Validate(request);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}