namespace Isu.Exceptions;

public class WrongCourseNumberException : Exception
{
    public WrongCourseNumberException(int courseNumber)
        : base("Error: Course Number is invalid!")
    { }
    public WrongCourseNumberException(int courseNumber, string message)
        : base(message)
    { }
}