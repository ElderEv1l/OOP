using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class OgnpGroupDoesntExistException : Exception
{
    public OgnpGroupDoesntExistException(OgnpGroup ognpGroup)
        : base($"Error: This Ognp Group doesn't exist!")
    { }
    public OgnpGroupDoesntExistException(OgnpGroup ognpGroup, string message)
        : base(message)
    { }
}