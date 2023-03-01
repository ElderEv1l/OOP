namespace Banks.Notifications.cs;

public interface INotification
{
    public string TypeOfProduct { get; }
    public string Message { get; }
}