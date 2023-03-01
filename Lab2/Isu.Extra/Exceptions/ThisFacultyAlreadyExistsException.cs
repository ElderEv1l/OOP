namespace Isu.Extra.Exceptions;

public class ThisFacultyAlreadyExistsException : Exception
{
    public ThisFacultyAlreadyExistsException(string facultyName)
        : base($"Error: Faculty With This Name Already Exists!")
    { }
    public ThisFacultyAlreadyExistsException(string facultyName, string message)
        : base(message)
    { }

    public ThisFacultyAlreadyExistsException(char facultyLetter)
        : base($"Error: Faculty With This Letter Already Exists!")
    { }
    public ThisFacultyAlreadyExistsException(char facultyLetter, string message)
        : base(message)
    { }
}