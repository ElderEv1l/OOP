using Shops.Exceptions;

namespace Shops.Entities;

public class Customer
{
    public Customer(string name, decimal balance)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyInputParameterException(nameof(name));
        }

        if (balance < 0)
        {
            throw new NegativeAmountOfMoneyException(balance);
        }

        Name = name;
        Balance = balance;
    }

    public string Name { get; }
    public decimal Balance { get; private set; }

    public void SpendBalance(decimal money)
    {
        if (money < 0)
        {
            throw new NegativeAmountOfMoneyException(money);
        }

        if (Balance < money)
        {
            throw new NotEnoughMoneyException(Balance, money);
        }

        Balance -= money;
    }

    public void AddBalance(decimal money)
    {
        if (money < 0)
        {
            throw new NegativeAmountOfMoneyException(money);
        }

        Balance += money;
    }
}