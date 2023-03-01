namespace Banks.Exceptions;

public class NegativeAmountOfDaysException : Exception
{
    public NegativeAmountOfDaysException(int days)
        : base($"Error: Days Can't be negative!")
    { }
    public NegativeAmountOfDaysException(int days, string message)
        : base(message)
    { }
}