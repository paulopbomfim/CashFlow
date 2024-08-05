using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ErrorMessagesResource.NAME_EMPTY);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.EMAIL_EMPTY)
            .EmailAddress()
            .When(user => !string.IsNullOrWhiteSpace(user.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ErrorMessagesResource.EMAIL_INVALID);

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RegisterUserRequest>());
    }
}