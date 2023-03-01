namespace Isu.Exceptions;

public class EmptyInputParameterException : Exception
{
    public EmptyInputParameterException(int parameterNumber)
        : base($"Error: At least one of this parameters is null!")
    { }
    public EmptyInputParameterException(int parameterNumber, string message)
        : base(message)
    { }
}