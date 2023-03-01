using Banks.Accounts;

namespace Banks.Transaction;

public interface ITransaction
{
    public Guid Id { get; }
    public IBankAccount FromWho { get; }
    public IBankAccount ToWho { get; }
    public decimal Money { get; }
    public DateTime DateAndTime { get; }
    public void Cancel(Guid id);
}