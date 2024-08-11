using CashFlow.Application.UseCases.Login;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Login.DoLogin;

public class DoLoginUseCaseTest
{
    private User User { get; set; }

    public DoLoginUseCaseTest()
    {
        User = UserBuilder.Build();
    }
    
    private static DoLoginUseCase CreateUseCase(User user, string? password = null)
    {
        var passwordEncrypter = new PasswordEncrypterBuilder().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();

        return new DoLoginUseCase(readRepository, passwordEncrypter, tokenGenerator);
    }
    
    [Fact]
    public async Task Success()
    {
        //Arrange
        var request = LoginRequestBuilder.Build();
        request.Email = User.Email;
        var useCase = CreateUseCase(User, request.Password);
        
        //Act
        var result = await useCase.Execute(request);
        
        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(User.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task Error_User_Not_Found()
    {
        //Arrange
        var request = LoginRequestBuilder.Build();
        var useCase = CreateUseCase(User, request.Password);
        
        //Act
        var act = async () => await useCase.Execute(request);
        
        //Assert
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 &&
            ex.GetErrors().Contains(ErrorMessagesResource.EMAIL_OR_PASSWORD_INVALID));
    }
    
    [Fact]
    public async Task Error_Password_Not_Match()
    {
        //Arrange
        var request = LoginRequestBuilder.Build();
        request.Email = User.Email;
        var useCase = CreateUseCase(User);
        
        //Act
        var act = async () => await useCase.Execute(request);
        
        //Assert
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 &&
            ex.GetErrors().Contains(ErrorMessagesResource.EMAIL_OR_PASSWORD_INVALID));
    }
}