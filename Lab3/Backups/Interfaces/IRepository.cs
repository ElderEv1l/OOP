using System.Security.Cryptography;
using Backups.Entities;

namespace Backups.Interfaces;

public interface IRepository
{
    public Storage SaveData(string backupTaskName, string restorePointName, IEnumerable<IBackupObject> backupObjects);
    public Storage SaveData(string backupTaskName, string restorePointName, IBackupObject backupObject);
    public void CheckFile(IBackupObject obj);
}