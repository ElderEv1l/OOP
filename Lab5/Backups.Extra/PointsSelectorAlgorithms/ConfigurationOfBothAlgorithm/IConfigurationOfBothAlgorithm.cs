using Backups.Entities;

namespace Backups.Extra.PointsSelectorAlgorithms.ConfigurationOfBothAlgorithm;

public interface IConfigurationOfBothAlgorithm
{
    public bool DoWeHaveToDoSomething(SelectByAmountOfPoints byAmountOfPoints, SelectByDate byDate, Backup backup);
    public IEnumerable<RestorePoint> Choose(SelectByAmountOfPoints byAmountOfPoints, SelectByDate byDate, Backup backup);
}