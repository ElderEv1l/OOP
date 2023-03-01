using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class Lesson
{
    public Lesson(string name, DayOfWeek day, LessonTimeEnum time, int room, string teacherName)
    {
        if (room <= 0)
        {
            throw new NegativeRoomException(room);
        }

        LessonName = name;
        LessonDay = day;
        LessonTime = time;

        Room = room;
        TeacherName = teacherName ?? throw new ArgumentNullException(nameof(teacherName));
    }

    public string LessonName { get; }
    public DayOfWeek LessonDay { get; }
    public LessonTimeEnum LessonTime { get; }
    public int Room { get; }
    public string TeacherName { get; }

    public bool IsLessonTimeEqual(Lesson lesson)
    {
        return lesson.LessonDay == LessonDay && lesson.LessonTime == LessonTime;
    }
}