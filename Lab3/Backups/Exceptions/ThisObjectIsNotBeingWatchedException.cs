using Backups.Interfaces;

namespace Backups.Exceptions;

public class ThisObjectIsNotBeingWatchedException : Exception
{
    public ThisObjectIsNotBeingWatchedException(string path)
        : base($"Error: This Object Is Not Being Watched In This Task!")
    { }
    public ThisObjectIsNotBeingWatchedException(string path, string message)
        : base(message)
    { }
    public ThisObjectIsNotBeingWatchedException(IBackupObject obj)
        : base($"Error: This Object Is Not Being Watched In This Task!")
    { }
    public ThisObjectIsNotBeingWatchedException(IBackupObject obj, string message)
        : base(message)
    { }
}