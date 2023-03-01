using Isu.Models;

namespace Isu.Exceptions;

public class GroupNotFoundException : Exception
{
    public GroupNotFoundException(string groupName)
        : base("Error: Group Not Found!")
    { }
    public GroupNotFoundException(string groupName, string message)
        : base(message)
    { }
    public GroupNotFoundException(CourseNumber groupName)
        : base("Error: Group Not Found!")
    { }
    public GroupNotFoundException(CourseNumber groupName, string message)
        : base(message)
    { }
}