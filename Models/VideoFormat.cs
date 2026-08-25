namespace WCRCorder.Models;

public sealed class VideoFormat
{
    public int Width { get; init; }

    public int Height { get; init; }

    public double MinFPS { get; init; }

    public double MaxFPS { get; init; }

    public string Codec { get; init; } = string.Empty;

    public override string ToString()
    {
        var fpsText = Math.Abs(MaxFPS - MinFPS) < 0.01
            ? $"{MaxFPS:0.##} FPS"
            : $"{MinFPS:0.##}–{MaxFPS:0.##} FPS";

        return $"{Width} × {Height} — {fpsText} — {Codec}";
    }
}