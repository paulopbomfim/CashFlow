using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities;

public class Expense
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public PaymentType PaymentType { get; init; }

    public long UserId { get; set; }
    public User User { get; init; } = default!;
}