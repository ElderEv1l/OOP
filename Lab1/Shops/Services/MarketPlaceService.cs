using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Services;

public class MarketPlaceService : IMarketplaceService
{
    private int _shopId = 0;
    private List<Shop> _shops;
    private List<Product> _products;
    public MarketPlaceService()
    {
        _shops = new List<Shop>();
        _products = new List<Product>();
    }

    public Shop AddShop(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyInputParameterException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new EmptyInputParameterException(nameof(address));
        }

        var newShop = new Shop(_shopId++, name, address);
        _shops.Add(newShop);
        return newShop;
    }

    public Customer AddCustomer(string name, decimal money)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyInputParameterException(nameof(name));
        }

        var customer = new Customer(name, money);
        return customer;
    }

    public Product AddProduct(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyInputParameterException(nameof(name));
        }

        var product = new Product(name);
        if (_products.All(prod => prod != product))
        {
            _products.Add(product);
        }

        return product;
    }

    public ProductInShop AddProductInShop(Product product, decimal price, int amount)
    {
        if (product == null)
        {
            throw new EmptyInputParameterException(nameof(product));
        }

        var prod = new ProductInShop(product, price, amount);
        return prod;
    }

    public void SupplyShop(Shop shop, ProductInShop[] products)
    {
        if (shop == null)
        {
            throw new EmptyInputParameterException(nameof(shop));
        }

        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        foreach (ProductInShop product in products)
        {
            shop.AddProduct(product);
        }
    }

    public void SupplyShop(Shop shop, List<ProductInShop> products)
    {
        if (shop == null)
        {
            throw new EmptyInputParameterException(nameof(shop));
        }

        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        foreach (ProductInShop product in products)
        {
            shop.AddProduct(product);
        }
    }

    public Order MakeOrder(Customer customer, Shop shop, ProductForCustomer[] products)
    {
        if (customer == null)
        {
            throw new EmptyInputParameterException(nameof(customer));
        }

        if (shop == null)
        {
            throw new EmptyInputParameterException(nameof(shop));
        }

        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        var order = new Order(customer, shop, products);
        order.MakePurchaseAndTakeProducts();

        return order;
    }

    public Order MakeOrder(Customer customer, Shop shop, List<ProductForCustomer> products)
    {
        if (customer == null)
        {
            throw new EmptyInputParameterException(nameof(customer));
        }

        if (shop == null)
        {
            throw new EmptyInputParameterException(nameof(shop));
        }

        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        var order = new Order(customer, shop, products);
        order.MakePurchaseAndTakeProducts();

        return order;
    }

    public Order MakeTheCheapestOrder(Customer customer, ProductForCustomer[] products)
    {
        if (customer == null)
        {
            throw new EmptyInputParameterException(nameof(customer));
        }

        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        return MakeOrder(customer, FindTheCheapest(products), products);
    }

    public Order MakeTheCheapestOrder(Customer customer, List<ProductForCustomer> products)
    {
        if (customer == null)
        {
            throw new EmptyInputParameterException(nameof(customer));
        }

        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        return MakeOrder(customer, FindTheCheapest(products), products);
    }

    public ProductInShop ChangePrice(Shop shop, Product product, decimal newPrice)
    {
        if (shop == null)
        {
            throw new EmptyInputParameterException(nameof(shop));
        }

        if (product == null)
        {
            throw new EmptyInputParameterException(nameof(product));
        }

        shop.ChangePrice(product, newPrice);
        return shop.GetProduct(product);
    }

    private Shop FindTheCheapest(ProductForCustomer[] products)
    {
        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        Shop? shopWithMinimalTotalCost = _shops.Where(shop => shop.DoWeHaveEnoughProductsInShop(products)).ToList().MinBy(shop => shop.GetTotalCost(products));

        if (shopWithMinimalTotalCost == null)
        {
            throw new NotEnoughProductsException(products);
        }

        return shopWithMinimalTotalCost;
    }

    private Shop FindTheCheapest(List<ProductForCustomer> products)
    {
        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        Shop? shopWithMinimalTotalCost = _shops.Where(shop => shop.DoWeHaveEnoughProductsInShop(products)).ToList().MinBy(shop => shop.GetTotalCost(products));

        if (shopWithMinimalTotalCost == null)
        {
            throw new NotEnoughProductsException(products);
        }

        return shopWithMinimalTotalCost;
    }
}