namespace CashFlow.Communication.Responses;

public record RegisteredUserResponse()
{
    public string Name { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}