using CashFlow.Application.UseCases.Users;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Users.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("              ")]
    [InlineData(null)]
    public void Error_Name_Empty(string name)
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        request.Name = name;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ErrorMessagesResource.NAME_EMPTY));
    }

    [Theory]
    [InlineData("")]
    [InlineData("              ")]
    [InlineData(null)]
    public void Error_Email_Empty(string email)
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        request.Email = email;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ErrorMessagesResource.EMAIL_EMPTY));
    }
    
    [Fact]
    public void Error_Email_Invalid()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        request.Email = "johndoe.com";
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ErrorMessagesResource.EMAIL_INVALID));
    }
    
    [Fact]
    public void Password_Empty()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        request.Password = string.Empty;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ErrorMessagesResource.INVALID_PASSWORD));
    }
}