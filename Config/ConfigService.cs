using System.Text.Json;
using WCRCorder.Models;
using WCRCorder.Utils;

namespace WCRCorder.Config;

public class ConfigService
{
    private const string DataFolder = AppPaths.Data;
    private const string ConfigFileName = "config.json";

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    public AppSettings Settings { get; private set; } = new();

    public void Load()
    {
        AppPaths.CreateDirectories();

        var configPath = AppPaths.Config;

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
        Directory.CreateDirectory(DataFolder);

        var configPath = Path.Combine(DataFolder, ConfigFileName);

        var json = JsonSerializer.Serialize(Settings, _jsonOptions);

        File.WriteAllText(configPath, json);
    }
}