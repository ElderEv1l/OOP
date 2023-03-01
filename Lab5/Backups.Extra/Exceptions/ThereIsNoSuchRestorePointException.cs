using Backups.Entities;

namespace Backups.Extra.Exceptions;

public class ThereIsNoSuchRestorePointException : Exception
{
    public ThereIsNoSuchRestorePointException(RestorePoint point)
        : base($"Error: There is no such point!")
    { }
    public ThereIsNoSuchRestorePointException(RestorePoint point, string message)
        : base(message)
    { }
}