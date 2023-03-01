using System.Text.RegularExpressions;
using Shops.Entities;
using Shops.Exceptions;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class MarketPlaceServiceTests
{
    private readonly MarketPlaceService _service;

    public MarketPlaceServiceTests()
    {
        _service = new MarketPlaceService();
    }

    [Fact]
    public void AddProductWithPrice_NegativeAmountOfMoney_ThrowException()
    {
        Product prod = _service.AddProduct("Customer");
        Assert.Throws<NegativeProductPriceException>(() => _service.AddProductInShop(prod, -10, 10));
    }

    [Fact]
    public void SupplyShop()
    {
        List<ProductInShop> products = MakeListOfProducts();
        Shop shop = MakeSupply(products);

        Assert.NotEmpty(shop.Products);
        Assert.Equal(10, shop.GetProduct(products.ElementAt(0).Product).Amount);
        Assert.Equal(11, shop.GetProduct(products.ElementAt(1).Product).Amount);
        Assert.Equal(12, shop.GetProduct(products.ElementAt(2).Product).Amount);
        Assert.Equal(5, shop.GetProduct(products.ElementAt(3).Product).Amount);
        Assert.Equal(6, shop.GetProduct(products.ElementAt(4).Product).Amount);
    }

    [Fact]
    public void MakeOrder()
    {
        List<ProductInShop> products = MakeListOfProducts();
        Shop shop = MakeSupply(products);

        Customer customer = _service.AddCustomer("Customer1", 20);
        customer.AddBalance(20);
        var productsToBuy = new List<ProductForCustomer>();
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(0).Product, 3));
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(1).Product, 4));
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(2).Product, 5));

        Order order = _service.MakeOrder(customer, shop, productsToBuy);

        Assert.Equal(7, shop.GetProduct(products.ElementAt(0).Product).Amount);
        Assert.Equal(7, shop.GetProduct(products.ElementAt(1).Product).Amount);
        Assert.Equal(7, shop.GetProduct(products.ElementAt(2).Product).Amount);
        Assert.Equal(5, shop.GetProduct(products.ElementAt(3).Product).Amount);
        Assert.Equal(6, shop.GetProduct(products.ElementAt(4).Product).Amount);
        Assert.Equal(14, customer.Balance);
    }

    [Fact]
    public void MakeOrder_NotEnoughMoney_ThrowException()
    {
        List<ProductInShop> products = MakeListOfProducts();
        Shop shop = MakeSupply(products);

        Customer customer = _service.AddCustomer("Customer1", 20);

        var productsToBuy = new List<ProductForCustomer>();
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(0).Product, 3));
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(1).Product, 4));
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(2).Product, 5));

        Assert.Throws<NotEnoughMoneyException>(() => _service.MakeOrder(customer, shop, productsToBuy));
    }

    [Fact]
    public void MakeOrder_NotEnoughProducts_ThrowException()
    {
        List<ProductInShop> products = MakeListOfProducts();
        Shop shop = MakeSupply(products);

        Customer customer = _service.AddCustomer("Customer1", 100);

        var productsToBuy = new List<ProductForCustomer>();
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(0).Product, 11));
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(1).Product, 4));
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(2).Product, 5));

        Assert.Throws<NotEnoughProductsException>(() => _service.MakeOrder(customer, shop, productsToBuy));
    }

    [Fact]
    public void MakeOrder_NoSuchProductInShop_ThrowException()
    {
        List<ProductInShop> products = MakeListOfProducts();
        Shop shop = MakeSupply(products);

        Customer customer = _service.AddCustomer("Customer1", 100);

        var productsToBuy = new List<ProductForCustomer>();
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(0).Product, 10));
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(1).Product, 4));
        productsToBuy.Add(new ProductForCustomer(products.ElementAt(2).Product, 5));
        productsToBuy.Add(new ProductForCustomer(new Product("Pogchamp"), 5));

        Assert.Throws<NoSuchProductInShopException>(() => _service.MakeOrder(customer, shop, productsToBuy));
    }

    [Fact]
    public void MakeTheCheapestOrder()
    {
        Shop shop1 = _service.AddShop("Shop1", "Address1");

        Product prod1 = _service.AddProduct("Potato");
        Product prod2 = _service.AddProduct("Tomato");

        var products1 = new List<ProductInShop>();
        products1.Add(_service.AddProductInShop(prod1, 10, 10));
        products1.Add(_service.AddProductInShop(prod2, 20, 11));
        _service.SupplyShop(shop1, products1);

        Shop shop2 = _service.AddShop("Shop1", "Address1");
        var products2 = new List<ProductInShop>();
        products2.Add(_service.AddProductInShop(prod1, 5, 10));
        products2.Add(_service.AddProductInShop(prod2, 10, 11));
        _service.SupplyShop(shop2, products2);

        Shop shop3 = _service.AddShop("Shop1", "Address1");
        var products3 = new List<ProductInShop>();
        products3.Add(_service.AddProductInShop(prod1, 1, 1));
        products3.Add(_service.AddProductInShop(prod2, 1, 11));
        _service.SupplyShop(shop3, products3);

        Customer customer = _service.AddCustomer("Customer1", 100);
        var productsToBuy = new List<ProductForCustomer>();
        productsToBuy.Add(new ProductForCustomer(prod1, 5));
        productsToBuy.Add(new ProductForCustomer(prod2, 6));

        Order order = _service.MakeTheCheapestOrder(customer, productsToBuy);

        Assert.Equal(shop2, order.ShopOfThisOrder);
        Assert.Equal(85, order.TotalCost);
    }

    [Fact]
    public void MakeTheCheapestOrder_NotEnoughProducts_ThrowException()
    {
        Shop shop1 = _service.AddShop("Shop1", "Address1");

        Product prod1 = _service.AddProduct("Potato");
        Product prod2 = _service.AddProduct("Tomato");

        var products1 = new List<ProductInShop>();
        products1.Add(_service.AddProductInShop(prod1, 10, 10));
        products1.Add(_service.AddProductInShop(prod2, 20, 11));
        _service.SupplyShop(shop1, products1);

        Shop shop2 = _service.AddShop("Shop1", "Address1");
        var products2 = new List<ProductInShop>();
        products2.Add(_service.AddProductInShop(prod1, 5, 10));
        products2.Add(_service.AddProductInShop(prod2, 10, 11));
        _service.SupplyShop(shop2, products2);

        Shop shop3 = _service.AddShop("Shop1", "Address1");
        var products3 = new List<ProductInShop>();
        products3.Add(_service.AddProductInShop(prod1, 1, 1));
        products3.Add(_service.AddProductInShop(prod2, 1, 11));
        _service.SupplyShop(shop3, products3);

        Customer customer = _service.AddCustomer("Customer1", 100);
        var productsToBuy = new List<ProductForCustomer>();
        productsToBuy.Add(new ProductForCustomer(prod1, 55));
        productsToBuy.Add(new ProductForCustomer(prod2, 6));

        Assert.Throws<NotEnoughProductsException>(() => _service.MakeTheCheapestOrder(customer, productsToBuy));
    }

    private List<ProductInShop> MakeListOfProducts()
    {
        Product prod1 = _service.AddProduct("Potato");
        Product prod2 = _service.AddProduct("Tomato");
        Product prod3 = _service.AddProduct("Corn");
        Product prod4 = _service.AddProduct("Cucumber");
        Product prod5 = _service.AddProduct("Celery");

        var products = new List<ProductInShop>();
        products.Add(_service.AddProductInShop(prod1, 1, 10));
        products.Add(_service.AddProductInShop(prod2, 2, 11));
        products.Add(_service.AddProductInShop(prod3, 3, 12));
        products.Add(_service.AddProductInShop(prod4, 8, 5));
        products.Add(_service.AddProductInShop(prod5, 9, 6));

        return products;
    }

    private Shop MakeSupply(List<ProductInShop> products)
    {
        Shop shop = _service.AddShop("Shop1", "Address1");

        _service.SupplyShop(shop, products);
        return shop;
    }
}