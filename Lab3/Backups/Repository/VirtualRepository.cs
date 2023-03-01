using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Repository;

public class VirtualRepository : IRepository
{
    private List<Storage> _storages = new List<Storage>();
    public IReadOnlyCollection<Storage> GetStorages => _storages;
    public Storage SaveData(string backupTaskName, string restorePointName, IEnumerable<IBackupObject> backupObjects)
    {
        if (backupTaskName == null) throw new ArgumentNullException(nameof(backupTaskName));
        if (restorePointName == null) throw new ArgumentNullException(nameof(restorePointName));
        if (backupObjects == null) throw new ArgumentNullException(nameof(backupObjects));

        var storage = new Storage(Path.Combine(backupTaskName, restorePointName, ".zip"));
        _storages.Add(storage);
        return storage;
    }

    public Storage SaveData(string backupTaskName, string restorePointName, IBackupObject backupObject)
    {
        if (backupTaskName == null) throw new ArgumentNullException(nameof(backupTaskName));
        if (restorePointName == null) throw new ArgumentNullException(nameof(restorePointName));
        if (backupObject == null) throw new ArgumentNullException(nameof(backupObject));

        var storage = new Storage(Path.Combine(backupTaskName, restorePointName, backupObject.GetObjectName(), ".zip"));
        _storages.Add(storage);
        return storage;
    }

    public void CheckFile(IBackupObject obj)
    {
        if (obj.Path.IndexOfAny(Path.GetInvalidPathChars()) != -1) throw new BackupObjectDoesntExists(obj.Path);
    }
}