using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses;

public class RegisterExpenseUseCase
{
    public RegisterExpenseResponse Execute(RegisterExpenseRequest request)
    {
        Validate(request);

        return new RegisterExpenseResponse()
        {
            Title = request.Title
        };
    }

    private void Validate(RegisterExpenseRequest request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}
