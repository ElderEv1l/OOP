namespace Shops.Exceptions;

public class NegativeAmountOfProductsException : Exception
{
    public NegativeAmountOfProductsException(int amount)
        : base("Error: Negative Amount Of Products!")
    { }
    public NegativeAmountOfProductsException(int amount, string message)
        : base(message)
    { }
}