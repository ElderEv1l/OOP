using System.Globalization;
using Backups.Entities;
using Backups.Extra.Exceptions;

namespace Backups.Extra.PointsSelectorAlgorithms;

public class SelectByAmountOfPoints : ISelectingAlgorithm
{
    public SelectByAmountOfPoints(int maxAmount)
    {
        if (maxAmount <= 0) throw new NegativeAmountException(maxAmount);

        MaxAmount = maxAmount;
    }

    public int MaxAmount { get; }

    public bool DoWeHaveToDoSomething(Backup backup)
    {
        if (backup == null) throw new ArgumentNullException(nameof(backup));

        return backup.GetRestorePoints.Count > MaxAmount;
    }

    public IEnumerable<RestorePoint> Choose(Backup backup)
    {
        if (backup == null) throw new ArgumentNullException(nameof(backup));

        IEnumerable<RestorePoint> sortedPoints = backup.GetRestorePoints.OrderByDescending(p => p.GetDateTime());
        return sortedPoints.SkipLast(backup.GetRestorePoints.Count - MaxAmount);
    }
}