using Backups.Entities;

namespace Backups.Extra.Restore;

public class ToOriginalRestoreAlgorithm : IRestoreAlgorithm
{
    private readonly BackupTask _backupTask;
    private readonly RestorePoint _restorePoint;

    public ToOriginalRestoreAlgorithm(BackupTask backupTask, RestorePoint restorePoint)
    {
        _backupTask = backupTask ?? throw new ArgumentNullException(nameof(backupTask));
        _restorePoint = restorePoint ?? throw new ArgumentNullException(nameof(restorePoint));
    }

    public void Execute()
    {
        foreach (Storage storage in _restorePoint.GetStorages)
        {
            var fileInfo = new FileInfo(storage.Path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            File.Create(storage.Path);
        }
    }

    public RestorePoint GetPoint()
    {
        return _restorePoint;
    }
}