using Isu.Models;

namespace Isu.Exceptions;

public class StudentNotFoundException : Exception
{
    public StudentNotFoundException(int id)
        : base("Error: Student Nor Found!")
    { }
    public StudentNotFoundException(int id, string message)
        : base(message)
    { }
    public StudentNotFoundException(CourseNumber courseNumber)
        : base()
    { }
    public StudentNotFoundException(CourseNumber courseNumber, string message)
        : base(message)
    { }
}