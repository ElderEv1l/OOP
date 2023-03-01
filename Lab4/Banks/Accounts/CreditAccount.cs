using Banks.Entities;
using Banks.Exceptions;
using Banks.Transaction;

namespace Banks.Accounts;

public class CreditAccount : IBankAccount
{
    private List<ITransaction> _transactions;
    private decimal _money;
    public CreditAccount(Client client, Bank bank, decimal money, decimal creditLimit)
    {
        Client = client;
        Bank = bank;
        _money = money;
        Cashback = 0;
        Commission = bank.CreditCommission;
        CreditLimit = creditLimit;
        Type = "Credit";
        _transactions = new List<ITransaction>();
    }

    public IReadOnlyCollection<ITransaction> GetTransactions => _transactions;
    public Client Client { get; }
    public string Type { get; }
    public Bank Bank { get; }
    public decimal Money => _money;
    public decimal Cashback { get; private set; }
    public int DaysGone { get; private set; }
    public decimal Commission { get; private set; }
    public decimal CreditLimit { get; }
    public void Withdraw(decimal money)
    {
        if (!IsWithdrawAllowed(money)) throw new OperationIsNotAvailableNowException(money);

        _money -= money;

        if (_money < 0)
            _money -= Commission;
    }

    public void CancelWithdraw(decimal money)
    {
        if (money <= 0) throw new NegativeAmountOfMoneyException(money);

        if (_money < 0)
            _money += Commission;
        _money += money;
    }

    public void TopUp(decimal money)
    {
        if (!IsTopUpAllowed(money)) throw new OperationIsNotAvailableNowException(money);

        _money += money;
    }

    public void CancelTopUp(decimal money)
    {
        if (money <= 0) throw new NegativeAmountOfMoneyException(money);

        _money -= money;
        if (_money < 0)
            _money -= Commission;
    }

    public void Transfer(decimal money, IBankAccount account, int id)
    {
        if (!IsWithdrawAllowed(money) || !account.IsTopUpAllowed(money)) return;
        Withdraw(money);
        if (_money < 0)
            _money -= Commission;
        account.TopUp(money);
        _transactions.Add(new TransferTransaction(this, account, money));
    }

    public void CancelTransfer(Guid id)
    {
        if (_transactions.All(trans => trans.Id != id)) throw new ThisTransferDoesntExistsException(id);

        ITransaction trans = _transactions.Single(t => t.Id == id);
        trans.ToWho.CancelTopUp(trans.Money);
        trans.FromWho.CancelWithdraw(trans.Money);
        _transactions.Remove(trans);
    }

    public bool IsTopUpAllowed(decimal money)
    {
        if (money <= 0) throw new NegativeAmountOfMoneyException(money);

        return true;
    }

    public bool IsWithdrawAllowed(decimal money)
    {
        if (money <= 0) throw new NegativeAmountOfMoneyException(money);

        if (money > _money + CreditLimit) return false;

        if (Client.IsDoubtful() && money > Bank.LimitForDoubtful) return false;

        return true;
    }

    public decimal GetLimit()
    {
        return _money + CreditLimit;
    }

    public void SkipDays(int days)
    {
    }

    public void RenewPercentages()
    {
        Commission = Bank.CreditCommission;
    }
}