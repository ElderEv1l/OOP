using Shops.Exceptions;

namespace Shops.Entities;

public class ProductInShop
{
    public ProductInShop(Product prod, decimal price, int amount)
    {
        if (price < 0)
        {
            throw new NegativeProductPriceException(price);
        }

        if (amount < 0)
        {
            throw new NegativeAmountOfProductsException(amount);
        }

        Product = prod;
        Price = price;
        Amount = amount;
    }

    public Product Product { get; }
    public decimal Price { get; private set; }
    public int Amount { get; private set; }

    public void IncreaseAmount(int amount)
    {
        Amount += amount;
    }

    public void DecreaseAmount(int amount)
    {
        Amount -= amount;
    }

    public void SetPrice(decimal newPrice)
    {
        Price = newPrice;
    }
}