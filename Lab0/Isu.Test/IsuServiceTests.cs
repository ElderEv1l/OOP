using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    private readonly IsuService _service;

    public IsuServiceTests()
    {
        _service = new IsuService();
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group group = _service.AddGroup("M32091");
        Student student = _service.AddStudent(group, "Gleb", "Kiryanov");

        Assert.Contains(student, group.Students!);
        Assert.Equal(group, student.Group);
    }

    [Theory]
    [InlineData("M32091", "Gleb", "Kiryanov")]
    [InlineData("M32091", "Evgeniy", "Novikov")]
    [InlineData("M32091", "Alexandr", "Malkov")]
    public void ReachMaxStudentPerGroup_ThrowException(string groupName, string firstName, string lastName)
    {
        Group group = _service.AddGroup(groupName);
        _service.AddStudent(group, firstName, lastName);
        _service.AddStudent(group, firstName, lastName);
        _service.AddStudent(group, firstName, lastName);
        _service.AddStudent(group, firstName, lastName);
        Assert.Throws<GroupOverFlowException>(() => _service.AddStudent(group, firstName, lastName));
    }

    [Theory]
    [InlineData("asd")]
    [InlineData("aasdsd")]
    [InlineData("123asd")]
    [InlineData("M35109")]
    [InlineData("M0324")]
    public void CreateGroupWithInvalidName_ThrowException(string invalidName)
    {
        Assert.Throws<InvalidGroupNameException>(() => _service.AddGroup(invalidName));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group oldGroup = _service.AddGroup("M32091");
        Group newGroup = _service.AddGroup("M32092");

        Student student = _service.AddStudent(oldGroup, "Gleb", "Kiryanov");
        _service.ChangeStudentGroup(student, newGroup);

        Assert.DoesNotContain(student, oldGroup.Students!);
        Assert.Contains(student, newGroup.Students!);
        Assert.Equal(newGroup, student.Group);
    }

    [Theory]
    [InlineData("M32091")]
    [InlineData("M32092")]
    [InlineData("M32093")]
    public void GroupAlreadyExists_ThrowException(string groupName)
    {
        _service.AddGroup(groupName);
        Assert.Throws<GroupAlreadyExistsException>(() => _service.AddGroup(groupName));
    }

    [Fact]
    public void StudentNotFound_ThrowException()
    {
        Group group = _service.AddGroup("M32091");
        _service.AddStudent(group, "Gleb", "Kiryanov");
        _service.AddStudent(group, "Alexandr", "Malkov");

        Assert.Throws<StudentNotFoundException>(() => _service.GetStudent(100005));
    }

    [Fact]
    public void GetStudent_Found()
    {
        Group group = _service.AddGroup("M32091");
        Student student = _service.AddStudent(group, "Gleb", "Kiryanov");

        Assert.Equal(student, _service.GetStudent(100000));
    }

    [Fact]
    public void FindStudent_Found()
    {
        Group group = _service.AddGroup("M32091");
        Student student = _service.AddStudent(group, "Gleb", "Kiryanov");

        Assert.Equal(student, _service.FindStudent(100000));
    }

    [Fact]
    public void FindStudent_NotFound()
    {
        Group group = _service.AddGroup("M32091");
        Student student = _service.AddStudent(group, "Gleb", "Kiryanov");

        Assert.Null(_service.FindStudent(100001));
    }

    [Fact]
    public void FindStudentsByCourseNumber()
    {
        Group group1 = _service.AddGroup("M32091");
        Group group2 = _service.AddGroup("M33092");
        _service.AddStudent(group2, "Gleb", "Kiryanov");

        Assert.Empty(_service.FindStudents(group1.CourseNumber) !);
    }
}