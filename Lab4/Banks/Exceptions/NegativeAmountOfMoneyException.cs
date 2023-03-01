namespace Banks.Exceptions;

public class NegativeAmountOfMoneyException : Exception
{
    public NegativeAmountOfMoneyException(decimal money)
        : base($"Error: Money In Transfer Can't be negative!")
    { }
    public NegativeAmountOfMoneyException(decimal money, string message)
        : base(message)
    { }
}