using System.Globalization;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<IBackupObject> _backupObjects;
    private readonly List<Storage> _storages;
    private DateTime _date;

    public RestorePoint(DateTime dateOfCreation)
    {
        _date = dateOfCreation;
        _backupObjects = new List<IBackupObject>();
        _storages = new List<Storage>();
    }

    public IReadOnlyCollection<IBackupObject> GetObjects => _backupObjects;
    public IReadOnlyCollection<Storage> GetStorages => _storages;

    public void AddObject(IBackupObject backupObject)
    {
        if (backupObject == null) throw new ArgumentNullException(nameof(backupObject));
        if (_backupObjects.Contains(backupObject)) throw new ThisObjectIsAlreadyBeingWatchedException(backupObject);

        _backupObjects.Add(backupObject);
    }

    public string GetTimeOfCreation()
    {
        return _date.ToString(CultureInfo.CurrentCulture);
    }

    public void AddStorage(Storage storage)
    {
        if (storage == null) throw new ArgumentNullException(nameof(storage));

        _storages.Add(storage);
    }

    public DateTime GetDateTime()
    {
        return _date;
    }
}