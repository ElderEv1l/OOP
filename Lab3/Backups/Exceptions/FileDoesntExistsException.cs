using Backups.Interfaces;

namespace Backups.Exceptions;

public class FileDoesntExistsException : Exception
{
    public FileDoesntExistsException(string path)
        : base($"Error: This File doesn't exist!")
    { }
    public FileDoesntExistsException(string path, string message)
        : base(message)
    { }
    public FileDoesntExistsException(IBackupObject obj)
        : base($"Error: File doesn't exist!")
    { }
    public FileDoesntExistsException(IBackupObject obj, string message)
        : base(message)
    { }
    public FileDoesntExistsException(List<IBackupObject> objects)
        : base($"Error: Some Files Are Missing!")
    { }
    public FileDoesntExistsException(List<IBackupObject> objects, string message)
        : base(message)
    { }
}