using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class Schedule
{
    private List<Lesson> _lessons;
    public Schedule()
    {
        _lessons = new List<Lesson>();
    }

    public IReadOnlyCollection<Lesson> Lessons => _lessons;

    public void AddLesson(Lesson lesson)
    {
        if (_lessons.Any(lesson.IsLessonTimeEqual))
        {
            throw new ScheduleCrossingException(lesson);
        }

        _lessons.Add(lesson);
    }
}