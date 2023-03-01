using Banks.Accounts;
using Banks.Exceptions;
using Banks.Notifications.cs;
using Banks.Transaction;

namespace Banks.Entities;

public class Bank
{
    private List<IBankAccount> _accounts;
    private List<Client> _subscribers;
    public Bank(string name, decimal creditCommission, decimal debitPercent, decimal depositPercent, decimal limitForDoubtful)
    {
        Name = name;
        _accounts = new List<IBankAccount>();
        CreditCommission = creditCommission;
        DebitPercent = debitPercent;
        DepositPercent = depositPercent;
        LimitForDoubtful = limitForDoubtful;
        _subscribers = new List<Client>();
    }

    public string Name { get; }
    public IReadOnlyCollection<IBankAccount> GetAccounts => _accounts;
    public decimal CreditCommission { get; private set; }
    public decimal DebitPercent { get; private set; }
    public decimal DepositPercent { get; private set; }
    public decimal DepositCoefficient { get; private set; }
    public decimal LimitForDoubtful { get; private set; }

    public void AddAccount(IBankAccount account)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));
        _accounts.Add(account);
    }

    public void SpendTime(int days)
    {
        if (days <= 0) throw new NegativeAmountOfDaysException(days);

        foreach (IBankAccount acc in _accounts)
        {
            acc.SkipDays(days);
        }
    }

    public ITransaction GetTransferById(Guid toFind)
    {
        if (!_accounts.Any(acc => acc.GetTransactions.Any(trans => trans.Id == toFind)))
            throw new ThisTransferDoesntExistsException(toFind);

        IBankAccount account = _accounts.Single(acc => acc.GetTransactions.Any(trans => trans.Id == toFind));
        return account.GetTransactions.Single(transaction => transaction.Id == toFind);
    }

    public void ChangeCreditPercent(decimal newCommission)
    {
        if (newCommission < 0) throw new NegativePercentsException(newCommission);

        CreditCommission = newCommission;
        SendNotifications("Credit", new CreditCommissionUpdate("Credit", newCommission));
        foreach (IBankAccount account in _accounts.Where(account => account.Type == "Credit"))
        {
            account.RenewPercentages();
        }
    }

    public void ChangeDebitPercent(decimal newPercent)
    {
        if (newPercent < 0) throw new NegativePercentsException(newPercent);

        DebitPercent = newPercent;
        SendNotifications("Debit", new PercentsUpdate("Debit", newPercent));
        foreach (IBankAccount account in _accounts.Where(account => account.Type == "Debit"))
        {
            account.RenewPercentages();
        }
    }

    public void ChangeDepositCoefficient(decimal newCoefficient)
    {
        if (newCoefficient < 0) throw new NegativePercentsException(newCoefficient);

        DepositCoefficient = newCoefficient;
        SendNotifications("Deposit", new DepositCoefficientUpdate("Deposit", newCoefficient));
        foreach (IBankAccount account in _accounts.Where(account => account.Type == "Deposit"))
        {
            account.RenewPercentages();
        }
    }

    public void ChangeDepositPercent(decimal newPercent)
    {
        if (newPercent < 0) throw new NegativePercentsException(newPercent);

        DepositPercent = newPercent;
        SendNotifications("Deposit", new PercentsUpdate("Deposit", newPercent));
        foreach (IBankAccount account in _accounts.Where(account => account.Type == "Deposit"))
        {
            account.RenewPercentages();
        }
    }

    public void ChangeLimitForDoubtful(decimal newLimit)
    {
        if (newLimit < 0) throw new NegativeAmountOfMoneyException(newLimit);

        DepositPercent = newLimit;
    }

    public void Subscribe(Client client)
    {
        if (client == null) throw new ArgumentNullException(nameof(client));

        _subscribers.Add(client);
    }

    public void SendNotifications(string type, INotification notification)
    {
        foreach (Client sub in _subscribers.Where(sub => sub.Accounts.Any(acc => acc.Type == type)))
        {
            sub.ReceiveNotification(notification);
        }
    }
}