using Isu.Entities;

namespace Isu.Extra.Exceptions;

public class StudentHasNoSuchOgnpGroupException : Exception
{
    public StudentHasNoSuchOgnpGroupException(Student student)
        : base($"Error: Student Has no Such a OgnpGroup!")
    { }
    public StudentHasNoSuchOgnpGroupException(Student student, string message)
        : base(message)
    { }
}