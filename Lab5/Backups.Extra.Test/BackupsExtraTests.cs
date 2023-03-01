using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.ActionWithPoint;
using Backups.Extra.Loggers;
using Backups.Extra.Loggers.LogMaker;
using Backups.Extra.PointsSelectorAlgorithms;
using Backups.Extra.Services;
using Backups.Interfaces;
using Backups.Repository;
using Xunit;

namespace Backups.Extra.Test;

public class BackupsExtraTests
{
    private IRepository _repository;
    private IAlgorithm _algorithm;
    private BackupTask _backupTask;

    private FileInfo _file1;
    private FileInfo _file2;

    public BackupsExtraTests()
    {
        _repository = new VirtualRepository();
        _algorithm = new SplitStorageAlgorithm();
        _backupTask = new BackupTask("name", _repository, _algorithm);

        File.Create("File1.txt").Close();
        File.Create("File2.txt").Close();
        _file1 = new FileInfo("File1.txt");
        _file2 = new FileInfo("File2.txt");

        _backupTask.AddBackupObject(new BackupFile(_file1.FullName));
        _backupTask.AddBackupObject(new BackupFile(_file2.FullName));
    }

    [Fact]
    public void FirstTest()
    {
        RestorePoint point1 = _backupTask.CreateRestorePointWithInfo(DateTime.Now);
        RestorePoint point2 = _backupTask.CreateRestorePointWithInfo(DateTime.Now);

        var logMaker = new LogWithoutPrefixMaker();
        var logger = new ConsoleLogger(logMaker);
        var backupTaskExtra1 = new BackupTaskExtra(_backupTask, logger, new SelectByAmountOfPoints(1), new DeletePoint());

        backupTaskExtra1.ControlPoints();
        Assert.Equal(1, _backupTask.GetRestorePoint.Count);
    }
}
