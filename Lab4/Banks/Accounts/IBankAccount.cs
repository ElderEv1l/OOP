using Banks.Entities;
using Banks.Transaction;

namespace Banks.Accounts;

public interface IBankAccount
{
    public IReadOnlyCollection<ITransaction> GetTransactions { get; }
    public Client Client { get; }
    public string Type { get; }
    public Bank Bank { get; }
    public decimal Money { get; }
    public void Withdraw(decimal money);
    public void CancelWithdraw(decimal money);
    public void TopUp(decimal money);
    public void CancelTopUp(decimal money);
    public void Transfer(decimal money, IBankAccount account, int id);
    public void CancelTransfer(Guid id);

    public bool IsTopUpAllowed(decimal money);
    public bool IsWithdrawAllowed(decimal money);

    public decimal GetLimit();
    public void SkipDays(int days);
    public void RenewPercentages();
}