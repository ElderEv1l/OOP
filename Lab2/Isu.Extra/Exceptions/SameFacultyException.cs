using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class SameFacultyException : Exception
{
    public SameFacultyException(OgnpGroup ognpGroup)
        : base($"Error: You can't join your faculty ognp!")
    { }
    public SameFacultyException(OgnpGroup ognpGroup, string message)
        : base(message)
    { }
}
