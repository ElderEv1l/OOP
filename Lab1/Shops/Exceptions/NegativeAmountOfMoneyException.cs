namespace Shops.Exceptions;

public class NegativeAmountOfMoneyException : Exception
{
    public NegativeAmountOfMoneyException(decimal money)
        : base("Error: Negative Amount Of Money!")
    { }
    public NegativeAmountOfMoneyException(decimal money, string message)
        : base(message)
    { }
}