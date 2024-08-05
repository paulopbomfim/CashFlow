using System.Net;

namespace CashFlow.Exception;

public class InvalidLoginException : CashFlowException
{
    public InvalidLoginException() : base(ErrorMessagesResource.EMAIL_OR_PASSWORD_INVALID)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public override List<string> GetErrors()
    {
        return [Message];
    }
}