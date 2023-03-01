using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class FacultyDoesntExistException : Exception
{
    public FacultyDoesntExistException(Faculty faculty)
        : base($"Error: This Faculty doesn't exist!")
    { }
    public FacultyDoesntExistException(Faculty faculty, string message)
        : base(message)
    { }
}