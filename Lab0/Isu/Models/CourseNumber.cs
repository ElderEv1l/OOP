using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    private const int MinNumberCourse = 1;
    private const int MaxNumberCourse = 4;
    private readonly int _number;

    public CourseNumber(int number)
    {
        if (number is >= MinNumberCourse and <= MaxNumberCourse)
        {
            _number = number;
        }
        else
        {
            throw new WrongCourseNumberException(number);
        }
    }

    public int GetCourseNumber()
    {
        return _number;
    }
}