namespace Banks.Exceptions;

public class BankAlreadyExistsException : Exception
{
    public BankAlreadyExistsException(string name)
        : base($"Error: This Bank Already Exists!")
    { }
    public BankAlreadyExistsException(string name, string message)
        : base(message)
    { }
}