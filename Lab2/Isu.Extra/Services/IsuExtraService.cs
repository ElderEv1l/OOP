using System.Runtime.CompilerServices;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService : IsuService
{
    private List<Faculty> _faculties = new List<Faculty>();

    public Faculty AddFaculty(string facultyName, char facultyLetter)
    {
        if (string.IsNullOrWhiteSpace(facultyName))
        {
            throw new ArgumentNullException(nameof(facultyName));
        }

        if (_faculties.Any(faculty => faculty.FacultyName == facultyName))
        {
            throw new ThisFacultyAlreadyExistsException(facultyName);
        }

        if (_faculties.Any(faculty => faculty.FacultyLetter == facultyLetter))
        {
            throw new ThisFacultyAlreadyExistsException(facultyLetter);
        }

        var faculty = new Faculty(facultyName, facultyLetter);
        _faculties.Add(faculty);
        return faculty;
    }

    public Ognp AddOgnp(string ognpName, Faculty faculty)
    {
        if (string.IsNullOrWhiteSpace(ognpName))
        {
            throw new ArgumentNullException(nameof(ognpName));
        }

        if (faculty == null) throw new ArgumentNullException(nameof(faculty));

        if (!_faculties.Contains(faculty))
        {
            throw new FacultyDoesntExistException(faculty);
        }

        if (faculty.Ognps.Any(ognp => ognp.OgnpName == ognpName))
        {
            throw new ThisOgnpAlreadyExistsException(ognpName);
        }

        var ognp = new Ognp(ognpName, faculty);
        faculty.AddOgnp(ognp);
        return ognp;
    }

    public OgnpGroup AddOgnpGroup(string groupName, Ognp ognp)
    {
        if (string.IsNullOrWhiteSpace(groupName))
        {
            throw new ArgumentNullException(nameof(groupName));
        }

        if (ognp == null) throw new ArgumentNullException(nameof(ognp));

        if (!_faculties.Any(fac => fac.Ognps.Contains(ognp)))
        {
            throw new OgnpDoesntExistException(ognp);
        }

        var ognpGroup = new OgnpGroup(groupName, ognp);
        ognp.AddOgnpGroup(ognpGroup);
        return ognpGroup;
    }

    public GroupExtra AddGroupExtra(string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
        {
            throw new ArgumentNullException(nameof(groupName));
        }

        Faculty? faculty = _faculties.SingleOrDefault(fac => fac.FacultyLetter == groupName[0]);
        if (faculty == null)
        {
            throw new InvalidGroupNameException(groupName);
        }

        var group = new GroupExtra(groupName);
        faculty.AddGroup(group);
        return group;
    }

    public void CreateLessonAndAddToGroupExtra(string name, DayOfWeek day, LessonTimeEnum time, int room, string teacherName, GroupExtra group)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(teacherName))
        {
            throw new ArgumentNullException(nameof(teacherName));
        }

        if (group == null) throw new ArgumentNullException(nameof(group));

        if (!_faculties.Any(fac => fac.Groups.Contains(group)))
        {
            throw new GroupExtraDoesntExistException(group);
        }

        var lesson = new Lesson(name, day, time, room, teacherName);
        group.AddLessonToGroupExtra(lesson);
    }

    public void CreateLessonAndAddToOgnpGroup(string name, DayOfWeek day, LessonTimeEnum time, int room, string teacherName, OgnpGroup group)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(teacherName))
        {
            throw new ArgumentNullException(nameof(teacherName));
        }

        if (group == null) throw new ArgumentNullException(nameof(group));

        if (!_faculties.Any(fac => fac.Ognps.Any(ognpp => ognpp.OgnpGroups.Contains(group))))
        {
            throw new OgnpGroupDoesntExistException(group);
        }

        var lesson = new Lesson(name, day, time, room, teacherName);
        group.AddLessonToOgnpGroup(lesson);
    }

    public void AddStudentToOgnp(Student student, OgnpGroup ognpGroup)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));
        if (ognpGroup == null) throw new ArgumentNullException(nameof(ognpGroup));

        if (!_faculties.Any(fac => fac.Ognps.Any(ognpp => ognpp.OgnpGroups.Contains(ognpGroup))))
        {
            throw new OgnpGroupDoesntExistException(ognpGroup);
        }

        if (student.Group != null && student.Group.GroupName[0] == ognpGroup.Ognp.Faculty.FacultyLetter)
        {
            throw new SameFacultyException(ognpGroup);
        }

        if (student.Group == null) throw new ArgumentNullException(nameof(student.Group));
        GroupExtra group = _faculties.Single(faculty => student.Group.GroupName[0] == faculty.FacultyLetter).Groups.Single(gr => gr.GroupName == student.Group.GroupName);
        List<OgnpGroup> studentsOgnpGroups = FindOgnpGroups(student);

        switch (studentsOgnpGroups.Count)
        {
            case >= 2:
                throw new TooManyOgnpGroupsException(student);
            case 1:
                if (!studentsOgnpGroups[0].IsScheduleFine(ognpGroup))
                {
                    throw new ScheduleCrossingException(ognpGroup.GroupSchedule);
                }

                if (studentsOgnpGroups[0].Ognp == ognpGroup.Ognp)
                {
                    throw new StudentAlreadyHasThisOgnpException(ognpGroup.Ognp);
                }

                break;
        }

        if (!group.IsScheduleFine(ognpGroup))
        {
            throw new ScheduleCrossingException(ognpGroup.GroupSchedule);
        }

        ognpGroup.AddStudent(student);
    }

    public void RemoveOgnpGroupFromStudent(Student student, OgnpGroup ognpGroup)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));
        if (ognpGroup == null) throw new ArgumentNullException(nameof(ognpGroup));

        List<OgnpGroup> studentsOgnpGroups = FindOgnpGroups(student);
        if (!studentsOgnpGroups.Contains(ognpGroup))
        {
            throw new StudentHasNoSuchOgnpGroupException(student);
        }

        ognpGroup.RemoveStudent(student);
    }

    public List<OgnpGroup> FindOgnpGroups(Student student)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));

        var studentsOgnpGroups = (from fac in _faculties from ognp in fac.Ognps from ogGr in ognp.OgnpGroups where ogGr.Students.Contains(student) select ogGr).ToList();
        return studentsOgnpGroups;
    }

    public IReadOnlyCollection<Student> FindStudents(OgnpGroup group)
    {
        if (group == null) throw new ArgumentNullException(nameof(group));

        if (!_faculties.Any(fac => fac.Ognps.Any(ognpp => ognpp.OgnpGroups.Contains(group))))
        {
            throw new OgnpGroupDoesntExistException(group);
        }

        return group.Students.Count == 0 ? new List<Student>() : group.Students;
    }

    public IReadOnlyCollection<Student> GetStudents(OgnpGroup group)
    {
        if (group == null) throw new ArgumentNullException(nameof(group));

        if (!_faculties.Any(fac => fac.Ognps.Any(ognpp => ognpp.OgnpGroups.Contains(group))))
        {
            throw new OgnpGroupDoesntExistException(group);
        }

        if (group.Students.Count == 0)
        {
            throw new GroupDoesntHaveAnyStudentException(group);
        }

        return group.Students;
    }

    public IReadOnlyCollection<OgnpGroup> FindOgnpGroups(Ognp ognp)
    {
        if (ognp == null) throw new ArgumentNullException(nameof(ognp));

        if (!_faculties.Any(fac => fac.Ognps.Contains(ognp)))
        {
            throw new OgnpDoesntExistException(ognp);
        }

        return ognp.OgnpGroups.Count == 0 ? new List<OgnpGroup>() : ognp.OgnpGroups;
    }

    public List<Student> FindStudentWithoutOgnpGroup()
    {
        var studentsWithoutOgnpGroup = new List<Student>();
        foreach (GroupExtra group in _faculties.SelectMany(faculty => faculty.Groups))
        {
            studentsWithoutOgnpGroup = group.Students.Where(student => FindOgnpGroups(student).Count < 2).ToList();
        }

        return studentsWithoutOgnpGroup;
    }
}