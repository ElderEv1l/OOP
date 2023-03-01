using Banks.Accounts;
using Banks.Notifications.cs;
using Banks.Transaction;

namespace Banks.Entities;

public class Client
{
    private readonly List<IBankAccount> _accounts;
    private readonly List<INotification> _notifications;

    public Client(string name, string address, string passport)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        Name = name;
        Address = address;
        Passport = passport;
        _accounts = new List<IBankAccount>();
        _notifications = new List<INotification>();
    }

    public string Name { get; }
    public string Address { get; private set; }
    public string Passport { get; private set; }
    public IReadOnlyCollection<IBankAccount> Accounts => _accounts;

    public bool IsDoubtful()
    {
        return string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Passport);
    }

    public void AddAccount(IBankAccount account)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));

        _accounts.Add(account);
    }

    public void ReceiveNotification(INotification notification)
    {
        if (notification == null) throw new ArgumentNullException(nameof(notification));
        _notifications.Add(notification);
    }
}