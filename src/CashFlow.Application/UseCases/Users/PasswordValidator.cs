using System.Text.RegularExpressions;
using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.Users;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ErrorMessageKey = "ErrorMessage";
    
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        var haveError = password switch
        {
            _ when string.IsNullOrWhiteSpace(password) => true,
            _ when password.Length < 8 => true,
            _ when !UppercaseRegex().IsMatch(password) => true,
            _ when !LowercaseRegex().IsMatch(password) => true,
            _ when !NumberRegex().IsMatch(password) => true,
            _ when !EspecialCharacteresRegex().IsMatch(password) => true,
            _ => false
        };

        if (!haveError) return true;
        
        context.MessageFormatter.AppendArgument(ErrorMessageKey, ErrorMessagesResource.INVALID_PASSWORD);
        return false;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UppercaseRegex();
    
    
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowercaseRegex();
    
    
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex NumberRegex();
    
    
    [GeneratedRegex(@"[\!\?\*\.]+")]
    private static partial Regex EspecialCharacteresRegex();
}