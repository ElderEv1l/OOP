using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Group = Isu.Entities.Group;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly Dictionary<string, Group> _groups;
        private int _studentId = 100000;
        public IsuService()
        {
            _groups = new Dictionary<string, Group>();
        }

        public Group AddGroup(string groupName)
        {
            if (groupName == null)
            {
                throw new EmptyInputParameterException(1);
            }

            if (_groups.ContainsKey(groupName))
            {
                throw new GroupAlreadyExistsException(groupName);
            }

            var newGroup = new Group(groupName);
            _groups.Add(groupName, newGroup);

            return newGroup;
        }

        public Student AddStudent(Group group, string firstName, string lastName)
        {
            if (group == null)
            {
                throw new EmptyInputParameterException(1);
            }

            if (firstName == null)
            {
                throw new EmptyInputParameterException(2);
            }

            if (lastName == null)
            {
                throw new EmptyInputParameterException(3);
            }

            var newStudent = new Student(firstName, lastName, group, _studentId++);
            group.Students?.Add(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            return _groups.Values.SelectMany(group => group.Students).FirstOrDefault(student => student.Id == id) ??
                 throw new StudentNotFoundException(id);
        }

        public Student? FindStudent(int id)
        {
            return _groups.Values.SelectMany(group => group.Students).FirstOrDefault(student => student.Id == id) ??
                   null;
        }

        public List<Student>? FindStudents(string groupName)
        {
            if (groupName == null)
            {
                throw new EmptyInputParameterException(1);
            }

            return _groups.ContainsKey(groupName) ? _groups[groupName].Students : new List<Student>();
        }

        public List<Student>? FindStudents(CourseNumber courseNumber)
        {
            if (courseNumber == null)
            {
                throw new EmptyInputParameterException(1);
            }

            IEnumerable<Group> groups = _groups.Values.Where(p => p.CourseNumber.GetCourseNumber().Equals(courseNumber.GetCourseNumber()));
            var students = groups.SelectMany(group => group.Students).ToList();
            return students;
        }

        public Group? FindGroup(string groupName)
        {
            if (groupName == null)
            {
                throw new EmptyInputParameterException(1);
            }

            return !_groups.ContainsKey(groupName) ? null : _groups[groupName];
        }

        public List<Group>? FindGroups(CourseNumber courseNumber)
        {
            if (courseNumber == null)
            {
                throw new EmptyInputParameterException(1);
            }

            return _groups.Values.Where(group => group.CourseNumber.GetCourseNumber() == courseNumber.GetCourseNumber()).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (student == null)
            {
                throw new EmptyInputParameterException(1);
            }

            if (newGroup == null)
            {
                throw new EmptyInputParameterException(2);
            }

            if (!_groups.ContainsValue(newGroup))
            {
                throw new GroupNotFoundException(newGroup.GroupName, "This group doesn't exist!");
            }

            if (newGroup.Students != null && newGroup.Students.Count >= Group.MaxStudents)
            {
                throw new GroupOverFlowException(newGroup.GroupName);
            }

            Group? oldGroup = student.Group;
            newGroup.Students?.Add(student);
            oldGroup?.Students?.Remove(student);
            student.Group = newGroup;
        }
    }
}
