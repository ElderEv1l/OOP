namespace Backups.Entities;

public class Backup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
    }

    public IReadOnlyCollection<RestorePoint> GetRestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (restorePoint == null) throw new ArgumentNullException(nameof(restorePoint));

        _restorePoints.Add(restorePoint);
    }

    public int CountStorages()
    {
        return _restorePoints.Sum(restorePoint => restorePoint.GetStorages.Count);
    }

    public void DeleteRestorePoint(RestorePoint point)
    {
        if (point == null) throw new ArgumentNullException(nameof(point));

        _restorePoints.Remove(point);
    }
}