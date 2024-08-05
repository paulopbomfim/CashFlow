namespace CashFlow.Communication.Requests.Login;

public record LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}