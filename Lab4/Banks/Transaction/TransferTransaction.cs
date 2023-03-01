using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transaction;

public class TransferTransaction : ITransaction
{
    public TransferTransaction(IBankAccount fromWho, IBankAccount toWho, decimal money)
    {
        if (money <= 0) throw new NegativeAmountOfMoneyException(money);

        Id = Guid.NewGuid();
        FromWho = fromWho ?? throw new ArgumentNullException(nameof(fromWho));
        ToWho = toWho ?? throw new ArgumentNullException(nameof(toWho));
        Money = money;
        IsTransferCancelled = false;
    }

    public Guid Id { get; }
    public IBankAccount FromWho { get; }
    public IBankAccount ToWho { get; }
    public decimal Money { get; }
    public DateTime DateAndTime => DateTime.Now;

    public bool IsTransferCancelled { get; }

    public void Cancel(Guid id)
    {
        FromWho.Bank.GetTransferById(id);

        FromWho.TopUp(Money);
        ToWho.Withdraw(Money);
    }
}