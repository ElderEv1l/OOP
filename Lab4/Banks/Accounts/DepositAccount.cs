using Banks.Entities;
using Banks.Exceptions;
using Banks.Transaction;

namespace Banks.Accounts;

public class DepositAccount : IBankAccount
{
    private List<ITransaction> _transactions;
    private decimal _money;
    public DepositAccount(Client client, Bank bank, decimal money, int months)
    {
        Client = client;
        Bank = bank;
        _money = money;
        ActualPercent = bank.DepositPercent + (bank.DepositCoefficient * money);
        MonthsLeft = months;
        Type = "Deposit";
        DaysGone = 0;
        Cashback = 0;
        _transactions = new List<ITransaction>();
    }

    public IReadOnlyCollection<ITransaction> GetTransactions => _transactions;
    public Client Client { get; }
    public string Type { get; }
    public Bank Bank { get; }
    public decimal Money => _money;
    public decimal ActualPercent { get; private set; }
    public int MonthsLeft { get; private set; }
    public decimal Cashback { get; private set; }
    public int DaysGone { get; private set; }

    public void Withdraw(decimal money)
    {
        if (!IsWithdrawAllowed(money)) throw new OperationIsNotAvailableNowException(money);

        _money -= money;
    }

    public void CancelWithdraw(decimal money)
    {
        if (money <= 0) throw new NegativeAmountOfMoneyException(money);

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
    }

    public void Transfer(decimal money, IBankAccount account, int id)
    {
        if (!IsWithdrawAllowed(money) || !account.IsTopUpAllowed(money)) return;
        Withdraw(money);
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

        if (money > _money) return false;

        if (Client.IsDoubtful() && money > Bank.LimitForDoubtful) return false;

        if (MonthsLeft != 0) return false;

        return true;
    }

    public decimal GetLimit()
    {
        return _money;
    }

    public void SkipDays(int days)
    {
        for (int i = 0; i < days; i++)
        {
            DaysGone++;
            if (DaysGone % 30 == 0)
            {
                PercentsHandlerByDay(true);
                if (MonthsLeft != 0)
                    MonthsLeft--;
            }
            else
            {
                PercentsHandlerByDay(false);
            }
        }
    }

    public void RenewPercentages()
    {
        ActualPercent = Bank.DepositPercent + (Bank.DepositCoefficient * _money);
    }

    private void PercentsHandlerByDay(bool ifLastDayOfMonth)
    {
        decimal dayPercent = ActualPercent / 365;
        Cashback += _money * dayPercent;

        if (!ifLastDayOfMonth) return;
        _money += Cashback;
        Cashback = 0;
    }
}