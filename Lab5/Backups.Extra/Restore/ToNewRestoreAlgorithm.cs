using Backups.Entities;

namespace Backups.Extra.Restore;

public class ToNewRestoreAlgorithm : IRestoreAlgorithm
{
    private readonly BackupTask _backupTask;
    private readonly RestorePoint _restorePoint;
    private readonly string _directoryPath;

    public ToNewRestoreAlgorithm(BackupTask backupTask, RestorePoint restorePoint, string directoryPath)
    {
        _backupTask = backupTask ?? throw new ArgumentNullException(nameof(backupTask));
        _restorePoint = restorePoint ?? throw new ArgumentNullException(nameof(restorePoint));
        _directoryPath = directoryPath ?? throw new ArgumentNullException(nameof(directoryPath));
    }

    public void Execute()
    {
        string path = Path.Combine(_directoryPath, $"{DateTime.Now}RestoredPoint");
        var directoryInfo = new DirectoryInfo(path);
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        foreach (Storage storage in _restorePoint.GetStorages)
        {
            File.Create(storage.Path);
        }
    }

    public RestorePoint GetPoint()
    {
        return _restorePoint;
    }
}