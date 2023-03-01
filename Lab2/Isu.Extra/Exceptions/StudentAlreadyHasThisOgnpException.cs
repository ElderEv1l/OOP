using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class StudentAlreadyHasThisOgnpException : Exception
{
    public StudentAlreadyHasThisOgnpException(Ognp ognp)
        : base($"Error: Student Already Has This Ognp!")
    { }
    public StudentAlreadyHasThisOgnpException(Ognp ognp, string message)
        : base(message)
    { }
}