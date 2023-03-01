using Banks.Exceptions;

namespace Banks.Entities;

public class CentralBank
{
    private List<Bank> _banks;

    public CentralBank(decimal creditPercent, decimal debitPercent, decimal depositPercent, decimal depositCoefficient, decimal limitForDoubtful)
    {
        _banks = new List<Bank>();
        CreditPercent = creditPercent;
        DebitPercent = debitPercent;
        DepositPercent = depositPercent;
        DepositCoefficient = depositCoefficient;
        LimitForDoubtful = limitForDoubtful;
    }

    public decimal CreditPercent { get; private set; }
    public decimal DebitPercent { get; private set; }
    public decimal DepositPercent { get; private set; }
    public decimal DepositCoefficient { get; private set; }
    public decimal LimitForDoubtful { get; private set; }

    public IReadOnlyCollection<Bank> GetBanks => _banks;

    public Bank CreateBank(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        if (_banks.Any(bank => bank.Name == name)) throw new BankAlreadyExistsException(name);

        var bank = new Bank(name, CreditPercent, DebitPercent, DepositPercent, LimitForDoubtful);
        _banks.Add(bank);

        return bank;
    }

    public void SpendTime(int days)
    {
        foreach (Bank bank in _banks)
        {
            bank.SpendTime(days);
        }
    }

    public void ChangeCreditPercent(decimal newPercent)
    {
        if (newPercent < 0) throw new NegativePercentsException(newPercent);

        CreditPercent = newPercent;
    }

    public void ChangeDebitPercent(decimal newPercent)
    {
        if (newPercent < 0) throw new NegativePercentsException(newPercent);

        DebitPercent = newPercent;
    }

    public void ChangeDepositPercent(decimal newPercent)
    {
        if (newPercent < 0) throw new NegativePercentsException(newPercent);

        DepositPercent = newPercent;
    }

    public void ChangeLimitForDoubtful(decimal newLimit)
    {
        if (newLimit < 0) throw new NegativeAmountOfMoneyException(newLimit);

        LimitForDoubtful = newLimit;
    }
}