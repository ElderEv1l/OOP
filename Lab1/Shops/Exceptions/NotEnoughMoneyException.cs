namespace Shops.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(decimal balance, decimal money)
        : base("Error: Not Enough Money!")
    { }
    public NotEnoughMoneyException(decimal balance, decimal money, string message)
        : base(message)
    { }
}