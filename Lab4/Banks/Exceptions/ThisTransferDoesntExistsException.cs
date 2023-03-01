namespace Banks.Exceptions;

public class ThisTransferDoesntExistsException : Exception
{
    public ThisTransferDoesntExistsException(Guid id)
        : base($"Error: This Transfer Doesn't Exists!")
    { }
    public ThisTransferDoesntExistsException(Guid id, string message)
        : base(message)
    { }
}