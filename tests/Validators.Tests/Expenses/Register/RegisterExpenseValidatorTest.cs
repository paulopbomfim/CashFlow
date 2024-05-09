using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTest
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = new RegisterExpenseRequest
        {
            Title = "ab quae sed",
            Description = "Dolor vero tenetur recusandae aut tempora.",
            Date = DateTime.Now.AddDays(-1),
            Amount = 1043.55,
            PaymentType = CashFlow.Communication.Enums.PaymentType.CreditCard
        };
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        Assert.True(result.IsValid);
    }
}