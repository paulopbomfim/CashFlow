namespace CashFlow.Exception;

public class ErrorOnValidationException : CashFlowException
{
    public List<string> Errors { get; set; }

    public ErrorOnValidationException(string errorMessages)
    {
        Errors = new List<string>() { errorMessages };
    }
    
    public ErrorOnValidationException(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
    
}
