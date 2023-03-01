using Backups.Entities;

namespace Backups.Extra.Restore;

public interface IRestoreAlgorithm
{
    public void Execute();
    public RestorePoint GetPoint();
}