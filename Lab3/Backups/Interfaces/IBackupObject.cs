using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackupObject
{
    public string Path { get; }
    public string GetObjectName();
    public string LogMessage();
}