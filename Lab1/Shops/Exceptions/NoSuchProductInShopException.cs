using Shops.Entities;

namespace Shops.Exceptions;

public class NoSuchProductInShopException : Exception
{
    public NoSuchProductInShopException(string name)
        : base($"Error: There Is No Such Product In This Shop!")
    { }
    public NoSuchProductInShopException(List<Product> products)
        : base($"Error: There Is No Such Product In This Shop!")
    { }
    public NoSuchProductInShopException(List<ProductForCustomer> products)
        : base($"Error: There Is No Such Product In This Shop!")
    { }
    public NoSuchProductInShopException(ProductForCustomer[] products)
        : base($"Error: There Is No Such Product In This Shop!")
    { }
    public NoSuchProductInShopException(List<Product> products, string message)
        : base(message)
    { }
}