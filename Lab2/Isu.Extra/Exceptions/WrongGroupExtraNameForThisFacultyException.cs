using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class WrongGroupExtraNameForThisFacultyException : Exception
{
    public WrongGroupExtraNameForThisFacultyException(string groupName)
        : base($"Error: This GroupName Doesn't fit to this faculty!")
    { }
    public WrongGroupExtraNameForThisFacultyException(string groupName, string message)
        : base(message)
    { }
}