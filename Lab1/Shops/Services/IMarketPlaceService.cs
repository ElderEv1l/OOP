using System.ComponentModel;
using Shops.Entities;

namespace Shops.Services;

public interface IMarketplaceService
{
    Shop AddShop(string name, string address);

    Customer AddCustomer(string name, decimal money);

    void SupplyShop(Shop shop, List<ProductInShop> products);

    Order MakeOrder(Customer customer, Shop shop, List<ProductForCustomer> products);

    Order MakeTheCheapestOrder(Customer customer, List<ProductForCustomer> products);
}
