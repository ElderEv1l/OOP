using System.Reflection;
using Isu.Entities;
using Isu.Extra.Exceptions;
namespace Isu.Extra.Entities;

public class GroupExtra : Group
{
    public GroupExtra(string groupName)
    : base(groupName)
    {
        GroupSchedule = new Schedule();
    }

    public Schedule GroupSchedule { get; }

    public void AddLessonToGroupExtra(Lesson lesson)
    {
        if (lesson == null) throw new ArgumentNullException(nameof(lesson));

        if (GroupSchedule.Lessons.Any(lesson.IsLessonTimeEqual))
        {
            throw new ScheduleCrossingException(lesson);
        }

        GroupSchedule.AddLesson(lesson);
    }

    public bool IsScheduleFine(OgnpGroup ognpGroup)
    {
        return !ognpGroup.GroupSchedule.Lessons.Any(ognpLesson => GroupSchedule.Lessons.Any(ognpLesson.IsLessonTimeEqual));
    }
}