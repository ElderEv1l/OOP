using System.Text;
using Backups.Extra.Loggers.LogMaker;

namespace Backups.Extra.Loggers;

public class FileLogger : ILogger
{
    public FileLogger(ILogMaker logMaker, string path)
    {
        LogMaker = logMaker;
        Path = path;
    }

    public ILogMaker LogMaker { get; }
    public string Path { get; }

    public void LogInformation(string info)
    {
        if (string.IsNullOrWhiteSpace(info)) throw new ArgumentNullException(nameof(info));

        string log = LogMaker.MakeLog(info);
        using var writer = new StreamWriter(File.OpenWrite(Path));
        writer.WriteLine(log);
    }
}