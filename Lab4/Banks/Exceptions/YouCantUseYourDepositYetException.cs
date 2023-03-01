namespace Banks.Exceptions;

public class YouCantUseYourDepositYetException : Exception
{
    public YouCantUseYourDepositYetException(int left)
        : base($"Error: You Still Can't Use Your Deposit!")
    { }
    public YouCantUseYourDepositYetException(int left, string message)
        : base(message)
    { }
}