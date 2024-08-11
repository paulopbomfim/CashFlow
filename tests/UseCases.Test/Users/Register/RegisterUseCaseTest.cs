using CashFlow.Application.UseCases.Users;
using CashFlow.Exception;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Users.Register;

public class RegisterUseCaseTest
{
    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var uow = UnitOfWorkBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncrypter = new PasswordEncrypterBuilder().Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder();

        if (!string.IsNullOrWhiteSpace(email))
        {
            readRepository.ExistsActiveUserWithEmail(email);
        }
        
        return new RegisterUserUseCase(mapper, passwordEncrypter, readRepository.Build(), writeRepository, tokenGenerator, uow);
    }

    [Fact]
    public async Task Success()
    {
        //Arrange
        var request = RegisterUserRequestBuilder.Build();
        var useCase = CreateUseCase();

        //Act
        var result = await useCase.Execute(request);
        
        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        //Arrange
        var request = RegisterUserRequestBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateUseCase();

        //Act
        var act = async () => await useCase.Execute(request);
        
        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => 
            ex.GetErrors().Count == 1 &&
            ex.GetErrors().Contains(ErrorMessagesResource.NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Email_Already_Exists()
    {
        //Arrange
        var request = RegisterUserRequestBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        //Act
        var act = async () => await useCase.Execute(request);
        
        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => 
            ex.GetErrors().Count == 1 &&
            ex.GetErrors().Contains(ErrorMessagesResource.EMAIL_ALREADY_REGISTERED));
    }
}