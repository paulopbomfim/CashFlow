namespace CashFlow.Exception;

public class NotFoundException : CashFlowException
{
    public NotFoundException(string message) : base(message) { }
}