namespace Banks.Exceptions;

public class NegativePercentsException : Exception
{
    public NegativePercentsException(decimal newPercent)
        : base($"Error: Percents Can't be negative!")
    { }
    public NegativePercentsException(decimal newPercent, string message)
        : base(message)
    { }
}
