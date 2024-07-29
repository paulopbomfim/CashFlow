using System.Text.RegularExpressions;
using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.Users;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ErrorMessageKey = "ErrorMessage";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ErrorMessageKey}}}";
    }

    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        var haveError = password switch
        {
            _ when string.IsNullOrWhiteSpace(password) => true,
            _ when password.Length < 8 => true,
            _ when !UpperCaseRegex().IsMatch(password) => true,
            _ when !LowerCaseRegex().IsMatch(password) => true,
            _ when !NumberRegex().IsMatch(password) => true,
            _ when !EspecialSymbolsRegex().IsMatch(password) => true,
            _ => false
        };

        if (!haveError) return true;
        
        context.MessageFormatter.AppendArgument(ErrorMessageKey, ErrorMessagesResource.INVALID_PASSWORD);
        return false;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseRegex();
    
    
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowerCaseRegex();
    
    
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex NumberRegex();
    
    
    [GeneratedRegex(@"[\!\?\*\.]+")]
    private static partial Regex EspecialSymbolsRegex();
}