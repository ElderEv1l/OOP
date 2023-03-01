using Backups.Algorithms;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Repository;
using Xunit;

namespace Backups.Test;

public class BackupsTest
{
    private IRepository _repository;
    private IAlgorithm _algorithm;
    private BackupTask _backupTask;

    private FileInfo _file1;
    private FileInfo _file2;

    public BackupsTest()
    {
        _repository = new VirtualRepository();
        _algorithm = new SplitStorageAlgorithm();
        _backupTask = new BackupTask("name", _repository, _algorithm);

        File.Create("File1.txt").Close();
        File.Create("File2.txt").Close();
        _file1 = new FileInfo("File1.txt");
        _file2 = new FileInfo("File2.txt");
    }

    [Fact]
    public void FirstTest()
    {
        Assert.Equal(0, _backupTask.GetObjectsToBackup.Count);

        var file1 = new BackupFile(_file1.FullName);
        var file2 = new BackupFile(_file2.FullName);

        _backupTask.AddBackupObject(file1);
        _backupTask.AddBackupObject(file2);
        Assert.Equal(2, _backupTask.GetObjectsToBackup.Count);
        Assert.Contains(file1, _backupTask.GetObjectsToBackup);
        Assert.Contains(file2, _backupTask.GetObjectsToBackup);

        _backupTask.CreateRestorePoint(DateTime.Now);
        Assert.Equal(1, _backupTask.GetRestorePoint.Count);
        Assert.Equal(2, _backupTask.BackupInfo.CountStorages());

        _backupTask.RemoveBackupObject(file2);
        _backupTask.CreateRestorePoint(DateTime.Now);
        Assert.Equal(3, _backupTask.BackupInfo.CountStorages());
        Assert.Equal(2, _backupTask.GetRestorePoint.Count);
    }
}