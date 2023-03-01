using System.IO.Compression;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    public Storage SaveData(string backupTaskName, string restorePointName, IEnumerable<IBackupObject> backupObjects)
    {
        if (backupTaskName == null) throw new ArgumentNullException(nameof(backupTaskName));
        if (restorePointName == null) throw new ArgumentNullException(nameof(restorePointName));
        if (backupObjects == null) throw new ArgumentNullException(nameof(backupObjects));

        string directoryPath = CreateRestorePointDirectory(backupTaskName, restorePointName);
        string archivePath = Path.Combine(directoryPath, restorePointName, ".zip");

        using (ZipArchive archive = ZipFile.Open(archivePath, ZipArchiveMode.Create))
        {
            foreach (IBackupObject backupObject in backupObjects)
            {
                archive.CreateEntryFromFile(backupObject.Path, backupObject.GetObjectName());
            }
        }

        return new Storage(archivePath);
    }

    public Storage SaveData(string backupTaskName, string restorePointName, IBackupObject backupObject)
    {
        if (backupTaskName == null) throw new ArgumentNullException(nameof(backupTaskName));
        if (restorePointName == null) throw new ArgumentNullException(nameof(restorePointName));
        if (backupObject == null) throw new ArgumentNullException(nameof(backupObject));

        string directoryPath = CreateRestorePointDirectory(backupTaskName, restorePointName);
        string archivePath = Path.Combine(directoryPath, restorePointName, backupObject.GetObjectName(), ".zip");

        using (ZipArchive archive = ZipFile.Open(archivePath, ZipArchiveMode.Create))
        {
            archive.CreateEntryFromFile(backupObject.Path, backupObject.GetObjectName());
        }

        return new Storage(archivePath);
    }

    public void CheckFile(IBackupObject obj)
    {
        if (!File.Exists(obj.Path) && !Directory.Exists(obj.Path)) throw new BackupObjectDoesntExists(obj.Path);
    }

    private void CreateBackupDirectory(string backupTaskName)
    {
        if (string.IsNullOrWhiteSpace(backupTaskName)) throw new ArgumentNullException(nameof(backupTaskName));

        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), backupTaskName)))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), backupTaskName));
        }
    }

    private string CreateRestorePointDirectory(string backupTaskName, string restorePointName)
    {
        if (string.IsNullOrWhiteSpace(backupTaskName)) throw new ArgumentNullException(nameof(backupTaskName));
        if (string.IsNullOrWhiteSpace(restorePointName)) throw new ArgumentNullException(nameof(restorePointName));

        CreateBackupDirectory(backupTaskName);

        string path = Path.Combine(Directory.GetCurrentDirectory(), backupTaskName, restorePointName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), backupTaskName, restorePointName));
        }

        return path;
    }
}