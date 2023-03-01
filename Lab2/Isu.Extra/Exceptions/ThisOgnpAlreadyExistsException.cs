namespace Isu.Extra.Exceptions;

public class ThisOgnpAlreadyExistsException : Exception
{
    public ThisOgnpAlreadyExistsException(string facultyName)
        : base($"Error: Ognp With This Name and Faculty Already Exists!")
    { }
    public ThisOgnpAlreadyExistsException(string facultyName, string message)
        : base(message)
    { }
}