using Backups.Entities;

namespace Backups.Extra.PointsSelectorAlgorithms.ConfigurationOfBothAlgorithm;

public class AllOfBothAlgorithms : IConfigurationOfBothAlgorithm
{
    public bool DoWeHaveToDoSomething(SelectByAmountOfPoints byAmountOfPoints, SelectByDate byDate, Backup backup)
    {
        return byAmountOfPoints.DoWeHaveToDoSomething(backup) && byDate.DoWeHaveToDoSomething(backup);
    }

    public IEnumerable<RestorePoint> Choose(SelectByAmountOfPoints byAmountOfPoints, SelectByDate byDate, Backup backup)
    {
        IEnumerable<RestorePoint> fromAmount = byAmountOfPoints.Choose(backup);
        IEnumerable<RestorePoint> fromDate = byDate.Choose(backup);

        IEnumerable<RestorePoint> result = fromAmount.Intersect(fromDate);

        return result;
    }
}