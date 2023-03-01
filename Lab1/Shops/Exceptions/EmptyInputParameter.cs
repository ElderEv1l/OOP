namespace Shops.Exceptions;

public class EmptyInputParameterException : Exception
{
    public EmptyInputParameterException(string parameterType)
        : base($"Error: At least one of this parameters is null!")
    { }
    public EmptyInputParameterException(string parameterType, string message)
        : base(message)
    { }
}