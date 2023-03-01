using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Extra.Loggers;
using Backups.Extra.PointsSelectorAlgorithms.ConfigurationOfBothAlgorithm;

namespace Backups.Extra.PointsSelectorAlgorithms;

public class SelectByBoth : ISelectingAlgorithm
{
    private readonly SelectByAmountOfPoints _byAmountOfPoints;
    private readonly SelectByDate _byDate;
    private readonly IConfigurationOfBothAlgorithm _configuration;

    public SelectByBoth(int maxAmount, DateTime lastAvailable, IConfigurationOfBothAlgorithm configuration)
    {
        if (maxAmount <= 0) throw new NegativeAmountException(maxAmount);

        _byAmountOfPoints = new SelectByAmountOfPoints(maxAmount);
        _byDate = new SelectByDate(lastAvailable);
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public bool DoWeHaveToDoSomething(Backup backup)
    {
        if (backup == null) throw new ArgumentNullException(nameof(backup));

        return _configuration.DoWeHaveToDoSomething(_byAmountOfPoints, _byDate, backup);
    }

    public IEnumerable<RestorePoint> Choose(Backup backup)
    {
        if (backup == null) throw new ArgumentNullException(nameof(backup));

        return _configuration.Choose(_byAmountOfPoints, _byDate, backup);
    }
}