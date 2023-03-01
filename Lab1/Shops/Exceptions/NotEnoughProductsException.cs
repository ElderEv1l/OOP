using Shops.Entities;

namespace Shops.Exceptions;

public class NotEnoughProductsException : Exception
{
    public NotEnoughProductsException(List<Product> products)
        : base("Error: Not Enough Products!")
    { }
    public NotEnoughProductsException(List<ProductForCustomer> products)
        : base("Error: Not Enough Products!")
    { }
    public NotEnoughProductsException(ProductForCustomer[] products)
        : base("Error: Not Enough Products!")
    { }
    public NotEnoughProductsException(List<Product> products, string message)
        : base(message)
    { }
}