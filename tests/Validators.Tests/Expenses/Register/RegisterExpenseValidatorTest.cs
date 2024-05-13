using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTest
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RegisterExpenseRequestBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("               ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RegisterExpenseRequestBuilder.Build();
        request.Title = title;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(vf => vf.ErrorMessage.Equals(ErrorMessagesResource.TITLE_REQUIRED));
    }
    
    [Fact]
    public void Error_Date_Future()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RegisterExpenseRequestBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(vf => vf.ErrorMessage.Equals(ErrorMessagesResource.EXPENSES_CANNOT_FOR_THE_FUTURE));
    }
    
    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RegisterExpenseRequestBuilder.Build();
        request.PaymentType = (PaymentType)700;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(vf => vf.ErrorMessage.Equals(ErrorMessagesResource.PAYMENT_TYPE_INVALID));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void Error_Amount_Invalid(double amount)
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RegisterExpenseRequestBuilder.Build();
        request.Amount = amount;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(vf => vf.ErrorMessage.Equals(ErrorMessagesResource.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}