using Backups.Entities;
using Backups.Extra.ActionWithPoint;
using Backups.Extra.Exceptions;
using Backups.Extra.Loggers;
using Backups.Extra.PointsSelectorAlgorithms;
using Backups.Extra.Restore;
using Backups.Interfaces;

namespace Backups.Extra.Services;

public class BackupTaskExtra
{
    public BackupTaskExtra(BackupTask backupTask, ILogger logger, ISelectingAlgorithm algorithm, IActionWithPoints action)
    {
        BackupTask = backupTask ?? throw new ArgumentNullException(nameof(backupTask));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        ActionWithPoints = action ?? throw new ArgumentNullException(nameof(action));
    }

    public BackupTask BackupTask { get; }
    public ILogger Logger { get; }
    public ISelectingAlgorithm Algorithm { get; }
    public IActionWithPoints ActionWithPoints { get; }

    public void AddObject(IBackupObject backupObject)
    {
        if (backupObject == null) throw new ArgumentNullException(nameof(backupObject));

        BackupTask.AddBackupObject(backupObject);
        Logger.LogInformation(backupObject.LogMessage() + "was added");
    }

    public void AddObjects(IEnumerable<IBackupObject> backupObjects)
    {
        if (backupObjects == null) throw new ArgumentNullException(nameof(backupObjects));

        foreach (IBackupObject backupObject in backupObjects)
        {
            BackupTask.AddBackupObject(backupObject);
            Logger.LogInformation(backupObject.LogMessage() + "was added");
        }
    }

    public RestorePoint CreateRestorePoint()
    {
        RestorePoint point = BackupTask.CreateRestorePointWithInfo(DateTime.Now);
        Logger.LogInformation($"Restore point from {point.GetDateTime()} was created");
        return point;
    }

    public void RestoreTheRestorePointToNew(IRestoreAlgorithm algo)
    {
        if (!BackupTask.GetRestorePoint.Contains(algo.GetPoint()))
        {
            throw new ThereIsNoSuchRestorePointException(algo.GetPoint());
        }

        algo.Execute();
    }

    public void ControlPoints()
    {
        ActionWithPoints.Execute(BackupTask, Algorithm);
    }
}