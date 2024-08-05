namespace CashFlow.Communication.Responses;

public record RegisteredUserResponse()
{
    public string Name { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}