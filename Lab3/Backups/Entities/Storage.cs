using Backups.Interfaces;

namespace Backups.Entities;

public class Storage
{
    public Storage(string path)
    {
        Path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public string Path { get; }
}