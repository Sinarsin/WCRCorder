using System.Text.Json;
using WCRCorder.Models;
using WCRCorder.Utils;

namespace WCRCorder.Config;

public class ConfigService
{

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    public AppSettings Settings { get; private set; } = new();

    public void Load()
    {
        AppPaths.CreateDirectories();

        var configPath = AppPaths.ConfigFile;

        if (!File.Exists(configPath))
        {
            Settings = new AppSettings();
            Save();
            return;
        }

        try
        {
            var json = File.ReadAllText(configPath);

            Settings = JsonSerializer.Deserialize<AppSettings>(json, _jsonOptions)
                       ?? new AppSettings();
        }
        catch
        {
            Settings = new AppSettings();
            Save();
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(AppPaths.DataDirectory);

        var configPath = Path.Combine(AppPaths.DataDirectory, AppPaths.ConfigFile);

        var json = JsonSerializer.Serialize(Settings, _jsonOptions);

        File.WriteAllText(configPath, json);
    }
}