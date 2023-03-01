namespace Backups.Extra.Exceptions;

public class NegativeAmountException : Exception
{
    public NegativeAmountException(int amount)
        : base($"Error: Amount can't be negative!")
    { }
    public NegativeAmountException(int amount, string message)
        : base(message)
    { }
}