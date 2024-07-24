using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;

namespace CashFlow.Domain.Extensions;

public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ReportPaymentTypeResource.CASH,
            PaymentType.CreditCard => ReportPaymentTypeResource.CREDIT_CARD,
            PaymentType.DebitCard => ReportPaymentTypeResource.DEBIT_CARD,
            PaymentType.ElectronicTransfer => ReportPaymentTypeResource.ELECTRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}