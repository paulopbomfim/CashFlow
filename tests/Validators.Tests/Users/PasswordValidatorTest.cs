using CashFlow.Application.UseCases.Users;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using FluentValidation;

namespace Validators.Tests.Users;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("              ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaa1")]
    public void Error_Password_Invalid(string password)
    {
        //Arrange
        var validator = new PasswordValidator<RegisterUserRequest>();
        
        
        //Act
        var result = validator.IsValid(new ValidationContext<RegisterUserRequest>(new RegisterUserRequest()), password);
        
        //Assert
        result.Should().BeFalse();
    }
}