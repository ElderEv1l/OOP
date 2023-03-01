using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Algorithms
{
    public class SplitStorageAlgorithm : IAlgorithm
    {
        public RestorePoint MakeStorage(string restorePointName, BackupTask backupTask)
        {
            if (restorePointName == null) throw new ArgumentNullException(nameof(restorePointName));
            if (backupTask == null) throw new ArgumentNullException(nameof(backupTask));

            var restorePoint = new RestorePoint(DateTime.Now);

            foreach (IBackupObject backupObject in backupTask.GetObjectsToBackup)
            {
                restorePoint.AddObject(backupObject);
                restorePoint.AddStorage(backupTask.Repository.SaveData(backupTask.Name, restorePointName, backupObject));
            }

            return restorePoint;
        }
    }
}