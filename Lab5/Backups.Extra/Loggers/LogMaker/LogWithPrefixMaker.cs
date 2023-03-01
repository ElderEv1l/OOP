namespace Backups.Extra.Loggers.LogMaker;

public class LogWithPrefixMaker : ILogMaker
{
    public string MakeLog(string log)
    {
        if (string.IsNullOrWhiteSpace(log)) throw new ArgumentNullException(nameof(log));

        return $"[{DateTime.Now.TimeOfDay}]" + log;
    }
}