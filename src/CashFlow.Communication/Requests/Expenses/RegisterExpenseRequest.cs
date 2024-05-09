﻿using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Requests;

public class RegisterExpenseRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}
