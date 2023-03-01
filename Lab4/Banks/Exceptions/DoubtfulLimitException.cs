namespace Banks.Exceptions;

public class DoubtfulLimitException : Exception
{
    public DoubtfulLimitException(decimal money)
        : base($"Error: You Can't Do This!")
    { }
    public DoubtfulLimitException(decimal money, string message)
        : base(message)
    { }
}