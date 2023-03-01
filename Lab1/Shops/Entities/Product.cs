using Shops.Exceptions;

namespace Shops.Entities;

public class Product
{
    public Product(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyInputParameterException(nameof(name));
        }

        Name = name;
    }

    public string Name { get; }
}