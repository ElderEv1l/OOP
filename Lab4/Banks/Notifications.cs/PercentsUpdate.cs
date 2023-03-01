namespace Banks.Notifications.cs;

public class PercentsUpdate : INotification
{
    public PercentsUpdate(string typeOfProduct, decimal percentNow)
    {
        TypeOfProduct = typeOfProduct;
        Message = "We have updated the percentages, now they are " + percentNow;
    }

    public string TypeOfProduct { get; }
    public string Message { get; }

    public override string ToString()
    {
        return $"{TypeOfProduct}: {Message}";
    }
}