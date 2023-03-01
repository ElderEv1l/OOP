using Backups.Interfaces;

namespace Backups.Exceptions;

public class ThisObjectIsAlreadyBeingWatchedException : Exception
{
    public ThisObjectIsAlreadyBeingWatchedException(string path)
        : base($"Error: This Object Is Already Being Watched In This Task!")
    { }
    public ThisObjectIsAlreadyBeingWatchedException(string path, string message)
        : base(message)
    { }
    public ThisObjectIsAlreadyBeingWatchedException(IBackupObject obj)
        : base($"Error: This Object Is Already Being Watched In This Task!")
    { }
    public ThisObjectIsAlreadyBeingWatchedException(IBackupObject obj, string message)
        : base(message)
    { }
}