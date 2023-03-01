namespace Backups.Extra.Loggers.LogMaker;

public class LogWithoutPrefixMaker : ILogMaker
{
    public string MakeLog(string log)
    {
        if (string.IsNullOrWhiteSpace(log)) throw new ArgumentNullException(nameof(log));

        return log;
    }
}