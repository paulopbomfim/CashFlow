﻿using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses;

public class RegisterExpenseValidator : AbstractValidator<RegisterExpenseRequest>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title)
            .NotEmpty().WithMessage(ErrorMessagesResource.TITLE_REQUIRED);

        RuleFor(expense => expense.Amount)
            .GreaterThan(0).WithMessage(ErrorMessagesResource.AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expense => expense.Date)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ErrorMessagesResource.EXPENSES_CANNOT_FOR_THE_FUTURE);

        RuleFor(expense => expense.PaymentType)
            .IsInEnum().WithMessage(ErrorMessagesResource.PAYMENT_TYPE_INVALID);
    }
}
