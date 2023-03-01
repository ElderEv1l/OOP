using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class Faculty
{
    private List<Ognp> _ognps = new List<Ognp>();
    private List<GroupExtra> _groups = new List<GroupExtra>();
    public Faculty(string name, char letter)
    {
        FacultyName = name;
        FacultyLetter = letter;
    }

    public string FacultyName { get; }
    public char FacultyLetter { get; }
    public IReadOnlyCollection<Ognp> Ognps => _ognps;
    public IReadOnlyCollection<GroupExtra> Groups => _groups;

    public void AddOgnp(Ognp ognp)
    {
        if (ognp == null) throw new ArgumentNullException(nameof(ognp));

        _ognps.Add(ognp);
    }

    public void AddGroup(GroupExtra group)
    {
        if (group == null) throw new ArgumentNullException(nameof(group));

        _groups.Add(group);
    }
}