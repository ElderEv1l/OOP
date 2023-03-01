namespace Banks.Notifications.cs;

public class CreditCommissionUpdate : INotification
{
    public CreditCommissionUpdate(string typeOfProduct, decimal commissionNow)
    {
        TypeOfProduct = typeOfProduct;
        Message = "We have updated the commission, now it is " + commissionNow;
    }

    public string TypeOfProduct { get; }
    public string Message { get; }

    public override string ToString()
    {
        return $"{TypeOfProduct}: {Message}";
    }
}