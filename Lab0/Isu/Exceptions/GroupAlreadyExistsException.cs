namespace Isu.Exceptions;

public class GroupAlreadyExistsException : Exception
{
    public GroupAlreadyExistsException(string groupName)
        : base($"Error: Group already exists!")
    { }
    public GroupAlreadyExistsException(string groupName, string message)
        : base(message)
    { }
}