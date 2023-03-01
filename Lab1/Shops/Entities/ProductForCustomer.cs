using Shops.Exceptions;

namespace Shops.Entities;

public class ProductForCustomer
{
    public ProductForCustomer(Product prod, int amount)
    {
        if (amount < 0)
        {
            throw new NegativeAmountOfProductsException(amount);
        }

        Product = prod;
        Amount = amount;
    }

    public Product Product { get; }
    public int Amount { get; private set; }

    public void IncreaseAmount(int amount)
    {
        Amount += amount;
    }

    public void DecreaseAmount(int amount)
    {
        if (Amount - amount < 0)
        {
            throw new NegativeAmountOfMoneyException(Amount - amount);
        }

        Amount -= amount;
    }
}