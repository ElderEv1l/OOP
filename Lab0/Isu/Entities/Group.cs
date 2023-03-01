using System.Text.RegularExpressions;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public const int MaxStudents = 4;
    private static readonly Regex GroupNameRegex = new Regex(@"^[A-Z][3][1-4]\d{2,3}$");
    public Group(string groupName)
    {
        MatchCollection matches = GroupNameRegex.Matches(groupName);
        if (matches.Count != 1 || groupName.Length >= 7) throw new InvalidGroupNameException(groupName);

        GroupName = groupName;
        Students = new List<Student>();
        CourseNumber = new CourseNumber(groupName[2] - '0');
    }

    public string GroupName { get; }

    public CourseNumber CourseNumber { get; }

    public List<Student> Students { get; set; }
}