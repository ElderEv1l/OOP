using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class OgnpGroup
{
    public const int MaxStudents = 4;
    private List<Student> _students = new List<Student>();
    public OgnpGroup(string ognpGroupName, Ognp ognp)
    {
        if (string.IsNullOrWhiteSpace(ognpGroupName))
        {
            throw new ArgumentNullException(nameof(ognpGroupName));
        }

        if (ognp == null) throw new ArgumentNullException(nameof(ognp));

        OgnpGroupName = ognpGroupName;
        Ognp = ognp ?? throw new ArgumentNullException(nameof(ognp));
        GroupSchedule = new Schedule();
    }

    public string OgnpGroupName { get; }
    public Ognp Ognp { get; }
    public Schedule GroupSchedule { get; }
    public IReadOnlyCollection<Student> Students => _students;

    public void AddLessonToOgnpGroup(Lesson lesson)
    {
        if (lesson == null) throw new ArgumentNullException(nameof(lesson));

        if (GroupSchedule.Lessons.Any(lesson.IsLessonTimeEqual))
        {
            throw new ScheduleCrossingException(lesson);
        }

        GroupSchedule.AddLesson(lesson);
    }

    public void AddStudent(Student student)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));

        if (_students.Count >= MaxStudents)
        {
            throw new GroupOverFlowException(OgnpGroupName);
        }

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));

        if (!_students.Contains(student))
        {
            throw new StudentHasNoSuchOgnpGroupException(student);
        }

        _students.Remove(student);
    }

    public bool IsScheduleFine(OgnpGroup ognpGroup)
    {
        return !ognpGroup.GroupSchedule.Lessons.Any(ognpLesson => GroupSchedule.Lessons.Any(ognpLesson.IsLessonTimeEqual));
    }
}