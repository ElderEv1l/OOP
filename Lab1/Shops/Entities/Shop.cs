using Shops.Exceptions;

namespace Shops.Entities;
public class Shop
{
    private List<ProductInShop> _products = new List<ProductInShop>();

    public Shop(int id, string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyInputParameterException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new EmptyInputParameterException(nameof(address));
        }

        Id = id;
        Name = name;
        Address = address;
    }

    public int Id { get; }
    public string Name { get; }
    public string Address { get; }
    public IReadOnlyCollection<ProductInShop> Products
    {
        get => _products;
    }

    public bool IsProductInShop(Product product)
    {
        if (product == null)
        {
            throw new EmptyInputParameterException(nameof(product));
        }

        return _products.Any(prod => prod.Product == product);
    }

    public bool IsProductInShop(ProductForCustomer product)
    {
        if (product == null)
        {
            throw new EmptyInputParameterException(nameof(product));
        }

        return _products.Any(prod => prod.Product == product.Product);
    }

    public bool DoWeHaveEnoughProductsInShop(ProductForCustomer[] products)
    {
        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        return products.All(prod => IsProductInShop(prod) && _products.Single(p => p.Product == prod.Product).Amount >= prod.Amount);
    }

    public bool DoWeHaveEnoughProductsInShop(List<ProductForCustomer> products)
    {
        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        return products.All(prod => IsProductInShop(prod) && _products.Single(p => p.Product == prod.Product).Amount >= prod.Amount);
    }

    public void AddProduct(ProductInShop newProduct)
    {
        if (newProduct == null)
        {
            throw new EmptyInputParameterException(nameof(newProduct));
        }

        if (IsProductInShop(newProduct.Product))
        {
            _products.Single(prod => prod.Product == newProduct.Product).IncreaseAmount(newProduct.Amount);
        }
        else
        {
            _products.Add(newProduct);
        }
    }

    public ProductInShop GetProduct(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyInputParameterException(nameof(name));
        }

        if (_products.All(prod => prod.Product.Name != name))
        {
            throw new NoSuchProductInShopException(name);
        }

        return _products.Single(prod => prod.Product.Name == name);
    }

    public ProductInShop GetProduct(Product product)
    {
        if (product == null)
        {
            throw new EmptyInputParameterException(nameof(product));
        }

        ProductInShop? prod = _products.SingleOrDefault(prod => prod.Product == product);
        if (prod == null)
        {
            throw new NoSuchProductInShopException(product.Name);
        }

        return prod;
    }

    public int AmountOfProduct(string nameOfProduct)
    {
        return _products.All(prod => prod.Product.Name != nameOfProduct) ? 0 : _products.Single(prod => prod.Product.Name == nameOfProduct).Amount;
    }

    public decimal GetTotalCost(ProductForCustomer[] products)
    {
        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        decimal totalCost = 0;
        if (products.Any(product => GetProduct(product.Product.Name).Amount < product.Amount))
        {
            throw new NotEnoughProductsException(products);
        }
        else
        {
            totalCost += products.Sum(product => GetProduct(product.Product.Name).Price * product.Amount);
        }

        return totalCost;
    }

    public decimal GetTotalCost(List<ProductForCustomer> products)
    {
        if (products == null)
        {
            throw new EmptyInputParameterException(nameof(products));
        }

        decimal totalCost = 0;
        if (products.Any(product => GetProduct(product.Product.Name).Amount < product.Amount))
        {
            throw new NotEnoughProductsException(products);
        }
        else
        {
            totalCost += products.Sum(product => GetProduct(product.Product.Name).Price * product.Amount);
        }

        return totalCost;
    }

    public void ChangePrice(Product product, decimal newPrice)
    {
        if (product == null)
        {
            throw new EmptyInputParameterException(nameof(product));
        }

        if (newPrice < 0)
        {
            throw new NegativeAmountOfMoneyException(newPrice);
        }

        GetProduct(product).SetPrice(newPrice);
    }
}