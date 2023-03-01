using Backups.Entities;

namespace Backups.Interfaces;

public interface IAlgorithm
{
    public RestorePoint MakeStorage(string restorePointName, BackupTask backupTask);
}