using Backups.Entities;

namespace Backups.Extra.PointsSelectorAlgorithms;

public interface ISelectingAlgorithm
{
    public bool DoWeHaveToDoSomething(Backup backup);
    public IEnumerable<RestorePoint> Choose(Backup backup);
}