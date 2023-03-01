using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class ScheduleCrossingException : Exception
{
    public ScheduleCrossingException(Lesson lesson)
        : base($"Error: There is crossing in this schedule!")
    { }
    public ScheduleCrossingException(Schedule schedule)
        : base($"Error: There is crossing in this schedule!")
    { }
    public ScheduleCrossingException(Lesson lesson, string message)
        : base(message)
    { }
}