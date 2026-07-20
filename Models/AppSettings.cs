namespace WCRCorder.Models;

public class AppSettings
{
    public string OutputFolder { get; set; } = "Data\\Video";

    public string VideoDevice { get; set; } = string.Empty;

    public string AudioDevice { get; set; } = string.Empty;

    public int Width { get; set; } = 1920;

    public int Height { get; set; } = 1080;

    public int FPS { get; set; } = 30;

    public string Codec { get; set; } = "libx264";

    public string Bitrate { get; set; } = "6000k";

    public int SegmentMinutes { get; set; } = 10;

    public bool StartHidden { get; set; } = false;

    public bool StartRecording { get; set; } = false;

    public string HotkeyShow { get; set; } = "Ctrl+Shift+4";

    public string HotkeyStart { get; set; } = "Ctrl+Shift+F8";

    public string HotkeyStop { get; set; } = "Ctrl+Shift+F9";

    public bool Logging { get; set; } = true;
}