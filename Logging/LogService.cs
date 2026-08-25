using System.Text;
using WCRCorder.Models;
using WCRCorder.Utils;

namespace WCRCorder.Logging;

public class LogService
{
    private readonly object _syncRoot = new();

    public void Write(string message, LogLevel level = LogLevel.Info)
    {
        try
        {
            AppPaths.CreateDirectories();

            var logFile = Path.Combine(
                AppPaths.LogsDirectory,
                $"{DateTime.Now:yyyy-MM-dd}.log");

            var line =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}{Environment.NewLine}";

            lock (_syncRoot)
            {
                File.AppendAllText(logFile, line, Encoding.UTF8);
            }
        }
        catch
        {
            // Логирование не должно приводить к аварийному завершению программы.
        }
    }
}