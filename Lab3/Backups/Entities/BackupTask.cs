using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<IBackupObject> _backupObjects;
    private int _restorePointNumber = 0;
    public BackupTask(string name, IRepository repository, IAlgorithm algo)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        Name = name;
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Algorithm = algo ?? throw new ArgumentNullException(nameof(algo));
        _backupObjects = new List<IBackupObject>();
        BackupInfo = new Backup();
    }

    public string Name { get; }
    public IRepository Repository { get; }
    public IAlgorithm Algorithm { get; }
    public Backup BackupInfo { get; }
    public IReadOnlyCollection<IBackupObject> GetObjectsToBackup => _backupObjects;

    public IReadOnlyCollection<RestorePoint> GetRestorePoint => BackupInfo.GetRestorePoints;

    public void AddBackupObject(IBackupObject backupObject)
    {
        if (backupObject == null) throw new ArgumentNullException(nameof(backupObject));
        if (_backupObjects.Any(@object => @object.Path == backupObject.Path)) throw new ThisObjectIsAlreadyBeingWatchedException(backupObject);

        _backupObjects.Add(backupObject);
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        if (backupObject == null) throw new ArgumentNullException(nameof(backupObject));
        if (_backupObjects.All(@object => @object.Path != backupObject.Path)) throw new ThisObjectIsNotBeingWatchedException(backupObject);

        _backupObjects.Remove(backupObject);
    }

    public void CreateRestorePoint(DateTime dateTime)
    {
        foreach (IBackupObject obj in _backupObjects)
        {
            Repository.CheckFile(obj);
        }

        RestorePoint point = Algorithm.MakeStorage($"({_restorePointNumber})", this);
        _restorePointNumber++;

        BackupInfo.AddRestorePoint(point);
    }

    public RestorePoint CreateRestorePointWithInfo(DateTime dateTime)
    {
        foreach (IBackupObject obj in _backupObjects)
        {
            Repository.CheckFile(obj);
        }

        RestorePoint point = Algorithm.MakeStorage($"({_restorePointNumber})", this);
        _restorePointNumber++;

        BackupInfo.AddRestorePoint(point);
        return point;
    }
}