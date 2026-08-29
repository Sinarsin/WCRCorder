using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using WCRCorder.Logging;
using WCRCorder.Models;
using WCRCorder.Utils;

namespace WCRCorder.Devices;

public sealed class DeviceService
{
    // логгер вывода ffmpeg
    private readonly LogService _logger = new();

    public IReadOnlyList<string> GetVideoDevices()
    {
        return GetDevices("video");
    }

    public IReadOnlyList<string> GetAudioDevices()
    {
        return GetDevices("audio");
    }

    public IReadOnlyList<VideoFormat> GetVideoFormats(string deviceName)
    {
        if (string.IsNullOrWhiteSpace(deviceName))
        {
            return Array.Empty<VideoFormat>();
        }

        var ffmpegPath = AppPaths.FFmpegExecutable;

        if (!File.Exists(ffmpegPath))
        {
            throw new FileNotFoundException(
                "FFmpeg executable was not found.",
                ffmpegPath);
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = ffmpegPath,
            Arguments =
                $"-hide_banner -f dshow -list_options true -i video=\"{deviceName}\"",
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardErrorEncoding = System.Text.Encoding.UTF8,
            StandardOutputEncoding = System.Text.Encoding.UTF8
        };

        using var process = new Process
        {
            StartInfo = startInfo
        };

        process.Start();

        var output = process.StandardError.ReadToEnd();

        _logger.Write($"FFmpeg formats output for '{deviceName}':");
        _logger.Write(output);

        process.WaitForExit();

        return ParseVideoFormats(output);
    }

    private IReadOnlyList<string> GetDevices(string type)
    {
        var ffmpegPath = AppPaths.FFmpegExecutable;

        if (!File.Exists(ffmpegPath))
        {
            throw new FileNotFoundException(
                "FFmpeg executable was not found.",
                ffmpegPath);
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = ffmpegPath,
            Arguments = "-hide_banner -list_devices true -f dshow -i dummy",
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true,

            StandardErrorEncoding = Encoding.UTF8,
            StandardOutputEncoding = Encoding.UTF8
        };

        using var process = new Process
        {
            StartInfo = startInfo
        };

        process.Start();

        var output = process.StandardError.ReadToEnd();

        process.WaitForExit();

 /*       _logger.Write("FFmpeg device enumeration output:");
        _logger.Write(output);
 */

        return ParseDevices(output, type);
    }

    private static IReadOnlyList<string> ParseDevices(
        string output,
        string type)
    {
        var devices = new List<string>();

        var lines = output.Split(
            new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var match = Regex.Match(
                line,
                "\"([^\"]+)\"\\s*\\((video|audio)\\)");

            if (!match.Success)
                continue;

            var deviceName = match.Groups[1].Value;
            var deviceType = match.Groups[2].Value;

            if (!string.Equals(deviceType, type, StringComparison.OrdinalIgnoreCase))
                continue;

            if (!devices.Contains(deviceName))
            {
                devices.Add(deviceName);
            }
        }

        return devices;
    }
    private static IReadOnlyList<VideoFormat> ParseVideoFormats(string output)
    {
        var formats = new List<VideoFormat>();

        var lines = output.Split(
            new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var mjpegMatch = Regex.Match(
                line,
                @"vcodec=(\w+)\s+min s=(\d+)x(\d+)\s+fps=([0-9.]+)\s+max s=\d+x\d+\s+fps=([0-9.]+)",
                RegexOptions.IgnoreCase);

            if (mjpegMatch.Success)
            {
                AddFormat(
                    formats,
                    mjpegMatch.Groups[1].Value,
                    mjpegMatch.Groups[2].Value,
                    mjpegMatch.Groups[3].Value,
                    mjpegMatch.Groups[4].Value,
                    mjpegMatch.Groups[5].Value);

                continue;
            }

            var yuyvMatch = Regex.Match(
                line,
                @"pixel_format=(\w+)\s+min s=(\d+)x(\d+)\s+fps=([0-9.]+)\s+max s=\d+x\d+\s+fps=([0-9.]+)",
                RegexOptions.IgnoreCase);

            if (yuyvMatch.Success)
            {
                AddFormat(
                    formats,
                    yuyvMatch.Groups[1].Value,
                    yuyvMatch.Groups[2].Value,
                    yuyvMatch.Groups[3].Value,
                    yuyvMatch.Groups[4].Value,
                    yuyvMatch.Groups[5].Value);
            }
        }

        return formats;
    }
    private static void AddFormat(
    List<VideoFormat> formats,
    string codec,
    string widthText,
    string heightText,
    string minFpsText,
    string maxFpsText)
    {
        if (!int.TryParse(widthText, out var width))
            return;

        if (!int.TryParse(heightText, out var height))
            return;

        if (!double.TryParse(
                minFpsText,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var minFps))
        {
            return;
        }

        if (!double.TryParse(
                maxFpsText,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var maxFps))
        {
            return;
        }

        var format = new VideoFormat
        {
            Width = width,
            Height = height,
            MinFPS = minFps,
            MaxFPS = maxFps,
            Codec = codec
        };

        if (!formats.Any(existing =>
                existing.Width == format.Width &&
                existing.Height == format.Height &&
                Math.Abs(existing.MinFPS - format.MinFPS) < 0.01 &&
                Math.Abs(existing.MaxFPS - format.MaxFPS) < 0.01 &&
                string.Equals(
                    existing.Codec,
                    format.Codec,
                    StringComparison.OrdinalIgnoreCase)))
        {
            formats.Add(format);
        }
    }
    public bool IsVideoDeviceAvailable(string deviceName)
    {
        if (string.IsNullOrWhiteSpace(deviceName))
            return false;

        var ffmpegPath = AppPaths.FFmpegExecutable;

        if (!File.Exists(ffmpegPath))
            return false;

        var startInfo = new ProcessStartInfo
        {
            FileName = ffmpegPath,

            Arguments =
                $"-hide_banner " +
                $"-f dshow " +
                $"-video_size 1920x1080 " +
                $"-vcodec mjpeg " +
                $"-i \"video={deviceName}\" " +
                $"-t 1 " +
                $"-f null -",

            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardErrorEncoding = Encoding.UTF8,
            StandardOutputEncoding = Encoding.UTF8
        };

        try
        {
            using var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();

            var error = process.StandardError.ReadToEnd();

            process.WaitForExit(3000);

            if (!process.HasExited)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                    // Ignore
                }

                _logger.Write(
                    $"Camera availability check timeout for '{deviceName}'.");

                return false;
            }

            _logger.Write(
                $"Camera availability check for '{deviceName}': exit code {process.ExitCode}");

            if (process.ExitCode != 0)
            {
                _logger.Write(error);
            }

            return process.ExitCode == 0;
        }
        catch (Exception ex)
        {
            _logger.Write(
                $"Camera availability check failed for '{deviceName}': {ex.Message}");

            return false;
        }
    }
}