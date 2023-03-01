using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class GroupDoesntHaveAnyStudentException : Exception
{
    public GroupDoesntHaveAnyStudentException(OgnpGroup ognpGroup)
        : base($"Error: You can't join your faculty ognp!")
    { }
    public GroupDoesntHaveAnyStudentException(OgnpGroup ognpGroup, string message)
        : base(message)
    { }
}