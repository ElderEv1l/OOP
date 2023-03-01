using Backups.Exceptions;
using Backups.Interfaces;
namespace Backups.Entities;

public class BackupDirectory : IBackupObject
{
    public BackupDirectory(string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (!Directory.Exists(path)) throw new DirectoryDoesntExistsException(path);

        Path = path;
    }

    public string Path { get; }

    public string GetObjectName()
    {
        return new DirectoryInfo(Path).Name;
    }

    public string LogMessage()
    {
        return $"Backup Directory at {Path}";
    }
}