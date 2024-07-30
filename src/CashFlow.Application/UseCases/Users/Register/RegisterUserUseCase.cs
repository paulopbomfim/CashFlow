using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _uow;
    private readonly IAccessTokenGenerator _tokenGenerator;
    public RegisterUserUseCase(
        IMapper mapper,
        IPasswordEncrypter passwordEncrypter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IAccessTokenGenerator tokenGenerator,
        IUnitOfWork uow)
    {
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _tokenGenerator = tokenGenerator;
        _uow = uow;
    }
    
    public async Task<RegisteredUserResponse> Execute(RegisterUserRequest request)
    {
        await Validate(request);
        
        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();
        
        await _userWriteOnlyRepository.Add(user);
        await _uow.Commit();
        
        return new RegisteredUserResponse
        {
            Name = user.Name,
            Token = _tokenGenerator.Generate(user)
        };
    }

    private async Task Validate(RegisterUserRequest request)
    {
        var result = await new RegisterUserValidator().ValidateAsync(request);

        var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ErrorMessagesResource.EMAIL_ALREADY_REGISTERED));
        }
        
        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}