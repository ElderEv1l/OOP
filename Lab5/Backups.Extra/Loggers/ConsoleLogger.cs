using Backups.Extra.Loggers.LogMaker;

namespace Backups.Extra.Loggers;

public class ConsoleLogger : ILogger
{
    public ConsoleLogger(ILogMaker logMaker)
    {
        LogMaker = logMaker;
    }

    public ILogMaker LogMaker { get; }

    public void LogInformation(string info)
    {
        if (string.IsNullOrWhiteSpace(info)) throw new ArgumentNullException(nameof(info));

        string log = LogMaker.MakeLog(info);
        Console.WriteLine(log);
    }
}