namespace Banks.Exceptions;

public class OperationIsNotAvailableNowException : Exception
{
    public OperationIsNotAvailableNowException(decimal money)
        : base($"Error: It's Not Allowed now!")
    { }
    public OperationIsNotAvailableNowException(decimal money, string message)
        : base(message)
    { }
}