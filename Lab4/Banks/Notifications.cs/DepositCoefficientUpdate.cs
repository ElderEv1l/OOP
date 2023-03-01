namespace Banks.Notifications.cs;

public class DepositCoefficientUpdate : INotification
{
    public DepositCoefficientUpdate(string typeOfProduct, decimal coefficientNow)
    {
        TypeOfProduct = typeOfProduct;
        Message = "We have updated the coefficient, now it is " + coefficientNow;
    }

    public string TypeOfProduct { get; }
    public string Message { get; }

    public override string ToString()
    {
        return $"{TypeOfProduct}: {Message}";
    }
}