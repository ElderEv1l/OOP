using Shops.Exceptions;

namespace Shops.Entities;

public class Order
{
    private List<ProductForCustomer> _products;
    public Order(Customer customerOfThisOrder, Shop shopOfThisOrder, List<ProductForCustomer> products)
    {
        if (products.Any(prod => !shopOfThisOrder.IsProductInShop(prod)))
        {
            throw new NoSuchProductInShopException(products);
        }

        if (!shopOfThisOrder.DoWeHaveEnoughProductsInShop(products))
        {
            throw new NotEnoughProductsException(products);
        }

        TotalCost = shopOfThisOrder.GetTotalCost(products);

        if (customerOfThisOrder.Balance < TotalCost)
        {
            throw new NotEnoughMoneyException(customerOfThisOrder.Balance, TotalCost);
        }

        CustomerOfThisOrder = customerOfThisOrder;
        ShopOfThisOrder = shopOfThisOrder;
        _products = products;
    }

    public Order(Customer customerOfThisOrder, Shop shopOfThisOrder, ProductForCustomer[] products)
    {
        if (products.Any(prod => !shopOfThisOrder.IsProductInShop(prod)))
        {
            throw new NoSuchProductInShopException(products);
        }

        if (!shopOfThisOrder.DoWeHaveEnoughProductsInShop(products))
        {
            throw new NotEnoughProductsException(products);
        }

        TotalCost = shopOfThisOrder.GetTotalCost(products);

        if (customerOfThisOrder.Balance < TotalCost)
        {
            throw new NotEnoughMoneyException(customerOfThisOrder.Balance, TotalCost);
        }

        CustomerOfThisOrder = customerOfThisOrder;
        ShopOfThisOrder = shopOfThisOrder;
        _products = products.ToList();
    }

    public Customer CustomerOfThisOrder { get; }
    public Shop ShopOfThisOrder { get; }
    public decimal TotalCost { get; }
    public IReadOnlyCollection<ProductForCustomer> Products
    {
        get => _products;
    }

    public decimal MakePurchaseAndTakeProducts()
    {
        CustomerOfThisOrder.SpendBalance(TotalCost);

        foreach (ProductForCustomer product in _products)
        {
            ShopOfThisOrder.GetProduct(product.Product.Name).DecreaseAmount(product.Amount);
        }

        return CustomerOfThisOrder.Balance;
    }
}