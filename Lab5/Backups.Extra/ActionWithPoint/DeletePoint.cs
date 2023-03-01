using Backups.Entities;
using Backups.Extra.PointsSelectorAlgorithms;

namespace Backups.Extra.ActionWithPoint;

public class DeletePoint : IActionWithPoints
{
    public void Execute(BackupTask backupTask, ISelectingAlgorithm algorithm)
    {
        Backup backup = backupTask.BackupInfo;
        if (backup == null) throw new ArgumentNullException(nameof(backup));
        if (algorithm == null) throw new ArgumentNullException(nameof(algorithm));

        IEnumerable<RestorePoint> toDelete = algorithm.Choose(backup);
        foreach (RestorePoint point in toDelete)
        {
            backup.DeleteRestorePoint(point);
        }
    }
}