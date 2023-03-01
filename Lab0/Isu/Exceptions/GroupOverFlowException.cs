namespace Isu.Exceptions;

public class GroupOverFlowException : Exception
{
    public GroupOverFlowException(string groupName)
        : base("Error: The group is full!")
    { }
    public GroupOverFlowException(string groupName, string message)
        : base(message)
    { }
}