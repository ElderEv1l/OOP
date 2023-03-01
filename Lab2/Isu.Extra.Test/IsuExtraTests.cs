using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Services;
using Xunit;
namespace Isu.Extra.Test;

public class IsuExtraTests
{
    private readonly IsuExtraService _service;

    public IsuExtraTests()
    {
        _service = new IsuExtraService();
    }

    [Fact]
    public void AddOgnpGroupFromSameFaculty_ThrowException()
    {
        Faculty faculty = _service.AddFaculty("Faculty", 'M');

        Ognp ognp = _service.AddOgnp("Ognp", faculty);
        OgnpGroup ognpGroup = _service.AddOgnpGroup("OgnpGroup", ognp);

        GroupExtra group = _service.AddGroupExtra("M32091");
        Student student = _service.AddStudent(group, "First", "Last");

        Assert.Throws<SameFacultyException>(() => _service.AddStudentToOgnp(student, ognpGroup));
    }

    [Fact]
    public void AddOgnpGroupFromSameOgnp_ThrowException()
    {
        Faculty faculty = _service.AddFaculty("Faculty1", 'M');
        Faculty faculty2 = _service.AddFaculty("Faculty2", 'F');

        Ognp ognp = _service.AddOgnp("Ognp", faculty2);
        OgnpGroup ognpGroup1 = _service.AddOgnpGroup("OgnpGroup", ognp);
        OgnpGroup ognpGroup2 = _service.AddOgnpGroup("OgnpGroup", ognp);

        GroupExtra group = _service.AddGroupExtra("M32091");
        Student student = _service.AddStudent(group, "First", "Last");

        _service.AddStudentToOgnp(student, ognpGroup1);
        Assert.Throws<StudentAlreadyHasThisOgnpException>(() => _service.AddStudentToOgnp(student, ognpGroup2));
    }

    [Fact]
    public void RemoveOgnpGroup_AndCheckStudentsWithoutOgnp()
    {
        Faculty faculty = _service.AddFaculty("Faculty1", 'M');
        Faculty faculty2 = _service.AddFaculty("Faculty2", 'F');

        Ognp ognp1 = _service.AddOgnp("Ognp", faculty2);
        OgnpGroup ognpGroup1 = _service.AddOgnpGroup("OgnpGroup", ognp1);
        Ognp ognp2 = _service.AddOgnp("Ognp2", faculty2);
        OgnpGroup ognpGroup2 = _service.AddOgnpGroup("OgnpGroup", ognp2);

        GroupExtra group = _service.AddGroupExtra("M32091");
        Student student = _service.AddStudent(group, "First", "Last");

        Assert.Contains(student, _service.FindStudentWithoutOgnpGroup());
        _service.AddStudentToOgnp(student, ognpGroup1);
        Assert.Contains(student, _service.FindStudentWithoutOgnpGroup());

        _service.AddStudentToOgnp(student, ognpGroup2);
        Assert.DoesNotContain(student, _service.FindStudentWithoutOgnpGroup());

        _service.RemoveOgnpGroupFromStudent(student, ognpGroup1);
        Assert.Contains(student, _service.FindStudentWithoutOgnpGroup());
    }

    [Fact]
    public void AddOgnpGroupAndStudentWithSchedule()
    {
        Faculty faculty1 = _service.AddFaculty("Faculty1", 'M');
        Faculty faculty2 = _service.AddFaculty("Faculty2", 'F');

        Ognp ognp = _service.AddOgnp("Ognp", faculty2);
        OgnpGroup ognpGroup1 = _service.AddOgnpGroup("OgnpGroup", ognp);
        _service.CreateLessonAndAddToOgnpGroup("Lesson1", DayOfWeek.Friday, LessonTimeEnum.First, 202, "Teacher1", ognpGroup1);
        _service.CreateLessonAndAddToOgnpGroup("Lesson1", DayOfWeek.Friday, LessonTimeEnum.Second, 202, "Teacher1", ognpGroup1);

        OgnpGroup ognpGroup2 = _service.AddOgnpGroup("OgnpGroup", ognp);
        _service.CreateLessonAndAddToOgnpGroup("Lesson1", DayOfWeek.Thursday, LessonTimeEnum.First, 202, "Teacher1", ognpGroup2);
        _service.CreateLessonAndAddToOgnpGroup("Lesson1", DayOfWeek.Thursday, LessonTimeEnum.Third, 202, "Teacher1", ognpGroup2);

        GroupExtra group = _service.AddGroupExtra("M32091");
        _service.CreateLessonAndAddToGroupExtra("Lesson1", DayOfWeek.Monday, LessonTimeEnum.First, 202, "Teacher1", group);
        _service.CreateLessonAndAddToGroupExtra("Lesson1", DayOfWeek.Monday, LessonTimeEnum.Second, 202, "Teacher1", group);

        Student student = _service.AddStudent(group, "First", "Last");
        _service.AddStudentToOgnp(student, ognpGroup1);
        Assert.Contains(student, ognpGroup1.Students);
    }

    [Fact]
    public void ScheduleCrossing_ThrowException()
    {
        Faculty faculty1 = _service.AddFaculty("Faculty1", 'M');
        Faculty faculty2 = _service.AddFaculty("Faculty2", 'F');

        Ognp ognp = _service.AddOgnp("Ognp", faculty2);
        OgnpGroup ognpGroup = _service.AddOgnpGroup("OgnpGroup", ognp);
        _service.CreateLessonAndAddToOgnpGroup("Lesson1", DayOfWeek.Monday, LessonTimeEnum.First, 202, "Teacher1", ognpGroup);
        _service.CreateLessonAndAddToOgnpGroup("Lesson1", DayOfWeek.Friday, LessonTimeEnum.Second, 202, "Teacher1", ognpGroup);

        GroupExtra group = _service.AddGroupExtra("M32091");
        _service.CreateLessonAndAddToGroupExtra("Lesson1", DayOfWeek.Monday, LessonTimeEnum.First, 202, "Teacher1", group);
        _service.CreateLessonAndAddToGroupExtra("Lesson1", DayOfWeek.Monday, LessonTimeEnum.Second, 202, "Teacher1", group);

        Student student = _service.AddStudent(group, "First", "Last");
        Assert.Throws<ScheduleCrossingException>(() => _service.AddStudentToOgnp(student, ognpGroup));
    }

    [Fact]
    public void FindOgnpGroupsByOgnp()
    {
        Faculty faculty = _service.AddFaculty("Faculty", 'M');

        Ognp ognp = _service.AddOgnp("Ognp", faculty);
        OgnpGroup ognpGroup1 = _service.AddOgnpGroup("OgnpGroup", ognp);
        OgnpGroup ognpGroup2 = _service.AddOgnpGroup("OgnpGroup", ognp);

        Assert.Contains(ognpGroup1, _service.FindOgnpGroups(ognp));
        Assert.Contains(ognpGroup2, _service.FindOgnpGroups(ognp));
    }
}