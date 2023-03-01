using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class TooManyOgnpGroupsException : Exception
{
    public TooManyOgnpGroupsException(Student student)
        : base($"Error: You have too many ognp groups!")
    { }
    public TooManyOgnpGroupsException(Student student, string message)
        : base(message)
    { }
}