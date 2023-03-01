namespace Backups.Exceptions;

public class BackupObjectDoesntExists : Exception
{
    public BackupObjectDoesntExists(string path)
        : base($"Error: This object doesn't exist!")
    { }
    public BackupObjectDoesntExists(string path, string message)
        : base(message)
    { }
}