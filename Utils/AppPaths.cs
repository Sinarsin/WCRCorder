namespace WCRCorder.Utils;

public static class AppPaths
{
    public const string Data = "Data";

    public static readonly string Logs = Path.Combine(Data, "Logs");

    public static readonly string Video = Path.Combine(Data, "Video");

    public static readonly string Temp = Path.Combine(Data, "Temp");

    public static readonly string Config = Path.Combine(Data, "config.json");

    public static void CreateDirectories()
    {
        Directory.CreateDirectory(Data);
        Directory.CreateDirectory(Logs);
        Directory.CreateDirectory(Video);
        Directory.CreateDirectory(Temp);
    }
}
