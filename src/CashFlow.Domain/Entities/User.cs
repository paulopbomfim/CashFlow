using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities;

public class User
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid UserIdentifier { get; set; }
    public string Role { get; init; } = Roles.TeamMember;
}