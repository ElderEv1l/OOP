using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class GroupExtraDoesntExistException : Exception
{
    public GroupExtraDoesntExistException(GroupExtra groupExtra)
        : base($"Error: This Group Extra doesn't exist!")
    { }
    public GroupExtraDoesntExistException(GroupExtra groupExtra, string message)
        : base(message)
    { }
}