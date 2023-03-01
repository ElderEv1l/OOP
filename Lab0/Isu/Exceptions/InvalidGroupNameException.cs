namespace Isu.Exceptions
{
    public class InvalidGroupNameException : Exception
    {
        public InvalidGroupNameException(string invalidGroupName)
            : base("Error: Group name is invalid!")
        { }
        public InvalidGroupNameException(string invalidGroupName, string message)
            : base(message)
        { }
    }
}
