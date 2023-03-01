namespace Shops.Exceptions;

public class NegativeProductPriceException : Exception
{
    public NegativeProductPriceException(decimal price)
        : base("Error: Negative Product Price!")
    { }
    public NegativeProductPriceException(decimal price, string message)
        : base(message)
    { }
}