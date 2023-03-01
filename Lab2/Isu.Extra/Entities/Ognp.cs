using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class Ognp
{
    private List<OgnpGroup> _ognpGroups = new List<OgnpGroup>();
    public Ognp(string ognpName, Faculty faculty)
    {
        OgnpName = ognpName;
        Faculty = faculty;
    }

    public string OgnpName { get; }
    public Faculty Faculty { get; }
    public IReadOnlyCollection<OgnpGroup> OgnpGroups => _ognpGroups;

    public void AddOgnpGroup(OgnpGroup ognpGroup)
    {
        if (ognpGroup == null) throw new ArgumentNullException(nameof(ognpGroup));

        _ognpGroups.Add(ognpGroup);
    }
}