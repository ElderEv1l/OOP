using Isu.Exceptions;

namespace Isu.Entities;

public class Student
{
    public Student(string firstName, string lastName, Group group, int id)
    {
        if (group.Students != null && group.Students.Count >= Group.MaxStudents)
        {
            throw new GroupOverFlowException(group.GroupName, "Group Over Flow!");
        }

        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Group = group;
    }

    public int Id { get; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Group? Group { get; set; }
}
