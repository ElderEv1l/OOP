using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.PointsSelectorAlgorithms;

namespace Backups.Extra.ActionWithPoint;

public class MergePoints : IActionWithPoints
{
    public void Execute(BackupTask backupTask, ISelectingAlgorithm algorithm)
    {
        Backup? backup = backupTask.BackupInfo;
        if (backup == null) throw new ArgumentNullException(nameof(backup));
        if (algorithm == null) throw new ArgumentNullException(nameof(algorithm));
        if (!backup.GetRestorePoints.Any()) return;

        IEnumerable<RestorePoint> toMerge = backup.GetRestorePoints;
        toMerge = toMerge.Except(algorithm.Choose(backup));

        var newPoint = new RestorePoint(DateTime.Now);
        if (algorithm.Choose(backup).Any())
        {
            newPoint = algorithm.Choose(backup).First();
        }
        else
        {
            backup.AddRestorePoint(newPoint);
        }

        if (!backup.GetRestorePoints.Except(algorithm.Choose(backup)).Any())
        {
            return;
        }

        if (backupTask.Algorithm is SingleStorageAlgorithm)
        {
            var deleter = new DeletePoint();
            deleter.Execute(backupTask, algorithm);
            return;
        }

        foreach (RestorePoint point in toMerge)
        {
            MergeNewPoint(point, newPoint);
            backup.DeleteRestorePoint(point);
        }
    }

    private void MergeNewPoint(RestorePoint oldPoint, RestorePoint newPoint)
    {
        if (oldPoint == null) throw new ArgumentNullException(nameof(oldPoint));
        if (newPoint == null) throw new ArgumentNullException(nameof(newPoint));

        foreach (Storage storage in oldPoint.GetStorages)
        {
            if (!newPoint.GetStorages.Contains(storage))
            {
                newPoint.AddStorage(storage);
            }
        }
    }
}