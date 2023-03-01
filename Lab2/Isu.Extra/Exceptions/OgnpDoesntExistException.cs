using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class OgnpDoesntExistException : Exception
{
    public OgnpDoesntExistException(Ognp ognp)
        : base($"Error: This Ognp doesn't exist!")
    { }
    public OgnpDoesntExistException(Ognp ognp, string message)
        : base(message)
    { }
}