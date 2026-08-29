using WCRCorder.FFmpeg;
using WCRCorder.Logging;
using WCRCorder.Models;

namespace WCRCorder.Recorder;

public sealed class RecorderService
{
    private readonly FFmpegService _ffmpeg;
    private readonly LogService _logger;

    public bool IsRecording => _ffmpeg.IsRunning;

    public RecorderService(
        FFmpegService ffmpeg,
        LogService logger)
    {
        _ffmpeg = ffmpeg;
        _logger = logger;
    }

    public void Start(
        AppSettings settings,
        string outputFile)
    {
        if (IsRecording)
        {
            _logger.Write(
                "Recording is already running.",
                LogLevel.Warning);

            return;
        }

        if (string.IsNullOrWhiteSpace(settings.VideoDevice))
        {
            throw new InvalidOperationException(
                "Video device is not configured.");
        }

        if (settings.Width <= 0 ||
            settings.Height <= 0)
        {
            throw new InvalidOperationException(
                "Invalid video resolution.");
        }

        if (settings.FPS <= 0)
        {
            throw new InvalidOperationException(
                "Invalid FPS value.");
        }

        if (settings.SegmentMinutes <= 0)
        {
            throw new InvalidOperationException(
                "Invalid segment duration.");
        }

        var arguments = BuildArguments(
            settings,
            outputFile);

        _logger.Write(
            $"Starting recording to '{outputFile}'.");

        _ffmpeg.Start(arguments);
    }

    public async Task StopAsync()
    {
        if (!IsRecording)
        {
            return;
        }

        _logger.Write("Stopping recording.");

        await _ffmpeg.StopAsync();
    }

    private static string BuildArguments(
        AppSettings settings,
        string outputFile)
    {
        var videoDevice =
            EscapeDeviceName(settings.VideoDevice);

        var audioDevice =
            EscapeDeviceName(settings.AudioDevice);

        var input = string.IsNullOrWhiteSpace(audioDevice)
            ? $"video={videoDevice}"
            : $"video={videoDevice}:audio={audioDevice}";

        return
            $"-hide_banner " +
            $"-f dshow " +
            (settings.UseWallClock
            ? "-use_wallclock_as_timestamps 1 ": "") +
            $"-video_size {settings.Width}x{settings.Height} " +
            (settings.AutoFPS ? "" : $"-framerate {settings.FPS} ") +
            $"-vcodec mjpeg " +
            $"-i \"{input}\" " +
            (settings.GeneratePTS? "-fflags +genpts ": "") +
            $"-c:v libx264 " +
            $"-preset veryfast " +
            $"-g {settings.GOP} " +
            $"-force_key_frames \"expr:gte(t,n_forced*{settings.ForceKeyFrameSeconds})\" " +
            $"-vf \"drawtext=font='Arial':text='%{{localtime\\:%Y-%m-%d %H\\\\\\:%M\\\\\\:%S}}':x=10:y=h-th-10:fontsize=24:fontcolor=white:borderw=2\" " +
            $"-pix_fmt yuv420p " +
            $"-b:v {settings.Bitrate} " +
            $"-c:a aac " +
     //       $"-af aresample=async=1000 " +
            $"-f segment " +
            $"-segment_time {settings.SegmentMinutes * 60} " +
            $"-reset_timestamps 1 " +
            $"-y " +
            $"\"{outputFile}\"";
    }

    private static string EscapeDeviceName(string name)
    {
        return name.Replace("\"", "\\\"");
    }

    public void Dispose()
    {
        _ffmpeg.Dispose();
    }
}