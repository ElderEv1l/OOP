using Backups.Entities;
using Backups.Extra.PointsSelectorAlgorithms;

namespace Backups.Extra.ActionWithPoint;

public interface IActionWithPoints
{
    public void Execute(BackupTask backupTask, ISelectingAlgorithm algorithm);
}