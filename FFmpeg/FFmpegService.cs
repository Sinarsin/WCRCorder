using System.Diagnostics;
using WCRCorder.Logging;
using WCRCorder.Utils;

namespace WCRCorder.FFmpeg;

public sealed class FFmpegService
{
    private readonly LogService _logger;

    private Process? _process;

    public bool IsRunning =>
        _process is { HasExited: false };

    public FFmpegService(LogService logger)
    {
        _logger = logger;
    }

    public void Start(string arguments)
    {
        if (IsRunning)
        {
            throw new InvalidOperationException(
                "FFmpeg is already running.");
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
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        _process = new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true
        };

        _process.Exited += FFmpegExited;

        _logger.Write($"Starting FFmpeg: {arguments}");

        if (!_process.Start())
        {
            _process.Dispose();
            _process = null;

            throw new InvalidOperationException(
                "Failed to start FFmpeg.");
        }

        _ = ReadErrorOutputAsync(_process);
    }

    public async Task StopAsync()
    {
        if (!IsRunning)
            return;

        _logger.Write("Stopping FFmpeg.");

        try
        {
            await _process!.StandardInput.WriteLineAsync("q");
            await _process.StandardInput.FlushAsync();

            await _process.WaitForExitAsync();
        }
        catch (Exception ex)
        {
            _logger.Write(
                $"Failed to stop FFmpeg gracefully: {ex.Message}",
                Models.LogLevel.Error);

            if (IsRunning)
            {
                try
                {
                    _process!.Kill(true);
                    await _process.WaitForExitAsync();
                }
                catch (Exception killEx)
                {
                    _logger.Write(
                        $"Failed to force stop FFmpeg: {killEx.Message}",
                        Models.LogLevel.Error);
                }
            }
        }
    }

    private async Task ReadErrorOutputAsync(Process process)
    {
        try
        {
            while (!process.HasExited)
            {
                var line = await process.StandardError.ReadLineAsync();

                if (line == null)
                    break;

                _logger.Write($"FFmpeg: {line}");
            }
        }
        catch (ObjectDisposedException)
        {
        }
        catch (Exception ex)
        {
            _logger.Write(
                $"Error reading FFmpeg output: {ex.Message}",
                Models.LogLevel.Error);
        }
    }

    private void FFmpegExited(
        object? sender,
        EventArgs e)
    {
        _logger.Write(
            $"FFmpeg process exited with code {_process?.ExitCode}.");

        _process?.Dispose();
        _process = null;
    }

    public void Dispose()
    {
        if (!IsRunning)
        {
            _process?.Dispose();
            _process = null;
            return;
        }

        try
        {
            _process!.StandardInput.WriteLine("q");
            _process.StandardInput.Flush();

            _process.WaitForExit();
        }
        catch
        {
            try
            {
                if (IsRunning)
                {
                    _process!.Kill(true);
                    _process.WaitForExit();
                }
            }
            catch
            {
                // Процесс уже завершается или завершился.
            }
        }
        finally
        {
            _process?.Dispose();
            _process = null;
        }
    }
}