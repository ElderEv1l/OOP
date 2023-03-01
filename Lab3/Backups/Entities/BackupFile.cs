using Backups.Exceptions;
using Backups.Interfaces;
namespace Backups.Entities;

public class BackupFile : IBackupObject
{
    public BackupFile(string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (!File.Exists(path)) throw new FileDoesntExistsException(path);

        Path = path;
    }

    public string Path { get; }

    public string GetObjectName()
    {
        return System.IO.Path.GetFileName(Path);
    }

    public string LogMessage()
    {
        return $"Backup File at {Path}";
    }
}