
namespace WCRCorder.Utils;

public static class AppPaths
{
#if DEBUG
    // Во время разработки используем папку проекта
    public static readonly string ApplicationFolder =
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
#else
    // В релизной версии используем папку EXE
    public static readonly string ApplicationFolder =
        AppContext.BaseDirectory;
#endif

    public static readonly string DataDirectory =
        Path.Combine(ApplicationFolder, "Data");

    public static readonly string LogsDirectory =
        Path.Combine(DataDirectory, "Logs");

    public static readonly string VideoDirectory =
        Path.Combine(DataDirectory, "Video");

    public static readonly string TempDirectory =
        Path.Combine(DataDirectory, "Temp");

    public static readonly string ConfigFile =
        Path.Combine(DataDirectory, "config.json");

    public static string FFmpegExecutable =>
        Path.Combine(AppContext.BaseDirectory, "FFmpeg", "Bin", "ffmpeg.exe");

    public static void CreateDirectories()
    {
        Directory.CreateDirectory(DataDirectory);
        Directory.CreateDirectory(LogsDirectory);
        Directory.CreateDirectory(VideoDirectory);
        Directory.CreateDirectory(TempDirectory);
    }
   
}