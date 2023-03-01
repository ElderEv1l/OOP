using System.Globalization;
using Backups.Entities;

namespace Backups.Extra.PointsSelectorAlgorithms;

public class SelectByDate : ISelectingAlgorithm
{
    public SelectByDate(DateTime lastAvailable)
    {
        LastAvailable = lastAvailable;
    }

    public DateTime LastAvailable { get; }

    public bool DoWeHaveToDoSomething(Backup backup)
    {
        if (backup == null) throw new ArgumentNullException(nameof(backup));

        return backup.GetRestorePoints.Any(point => point.GetDateTime() < LastAvailable);
    }

    public IEnumerable<RestorePoint> Choose(Backup backup)
    {
        if (backup == null) throw new ArgumentNullException(nameof(backup));

        IEnumerable<RestorePoint> sortedPoints = backup.GetRestorePoints.OrderByDescending(p => p.GetDateTime());
        return sortedPoints.Where(point =>
            point.GetDateTime().CompareTo(LastAvailable) < 0).ToList();
    }
}