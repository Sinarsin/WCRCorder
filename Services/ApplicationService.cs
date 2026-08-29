using WCRCorder;
using WCRCorder.Config;
using WCRCorder.Devices;
using WCRCorder.Logging;
using WCRCorder.Tray;
using WCRCorder.Utils;
using WCRCorder.FFmpeg;
using WCRCorder.Recorder;

namespace WCRCorder.Services;

public sealed class ApplicationService
{
    private MainForm? _mainForm;
    private bool _isShuttingDown;
    public TrayManager Tray { get; }
    public ConfigService Config { get; }
    public LogService Logger { get; }
    public ApplicationStateService State { get; }
    public RecorderService Recorder { get; }
    private System.Threading.Timer? _cameraStatusTimer;

    public void ShowSettings()
    {
        if (string.IsNullOrEmpty(Config.Settings.Password))
        {
            ShowSettingsForm();
            return;
        }

        using var passwordForm = new PasswordForm(Config.Settings.Password);

        if (passwordForm.ShowDialog() != DialogResult.OK)
            return;

        if (!passwordForm.IsPasswordValid)
        {
            MessageBox.Show(
                "Incorrect password.",
                "Settings",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        ShowSettingsForm();
    }

    private void ShowSettingsForm()
    {
        if (_mainForm == null)
        {
            _mainForm = new MainForm(this);
        }

        _mainForm.Show();
        _mainForm.BringToFront();
        _mainForm.Activate();
    }

    public void HideSettings()
    {
        _mainForm?.Hide();
    }

    public ApplicationService()
    {
        Config = new ConfigService();
        Logger = new LogService();
        State = new ApplicationStateService();

        var ffmpeg = new FFmpegService(Logger);

        Recorder = new RecorderService(
            ffmpeg,
            Logger);

        Tray = new TrayManager();


        Tray.StartRequested += StartRecording;
        Tray.StopRequested += StopRecording;
        Tray.SettingsRequested += ShowSettings;
        Tray.ExitRequested += Shutdown;
    }

    public void Initialize()
    {
        AppPaths.CreateDirectories();

        Config.Load();

        var devices = new DeviceService();

        var videoDevice = Config.Settings.VideoDevice;

        if (string.IsNullOrWhiteSpace(videoDevice))
        {
            Tray.SetStatus("Busy");
        }
        else
        {
            var available = devices.IsVideoDeviceAvailable(videoDevice);

            Tray.SetStatus(available ? "Ready" : "Busy");
        }

        _cameraStatusTimer = new System.Threading.Timer(
            _ => UpdateCameraStatus(),
            null,
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(5));

        Logger.Write("Application started. Initialize completed.");

        State.SetState(Models.ApplicationState.Ready);

        var videoDevices = devices.GetVideoDevices();
        var audioDevices = devices.GetAudioDevices();

        Logger.Write($"Video devices: {string.Join(", ", videoDevices)}");
        Logger.Write($"Audio devices: {string.Join(", ", audioDevices)}");

        //-временно
        if (videoDevices.Count > 0)
        {
            var formats = devices.GetVideoFormats(videoDevices[0]);

            Logger.Write(
                $"Video formats for '{videoDevices[0]}':");

            foreach (var format in formats)
            {
                Logger.Write($"  {format}");
            }
        }


    }

    private void StartRecording()
    {
        try
        {
            var settings = Config.Settings;

            if (string.IsNullOrWhiteSpace(settings.VideoDevice))
            {
                Logger.Write(
                    "Cannot start recording: video device is not configured.",
                    Models.LogLevel.Warning);

                Tray.SetStatus("Busy");
                return;
            }

            Tray.SetStatus("Working");

            var videoFolder = Path.Combine(
                settings.OutputFolder,
                DateTime.Now.ToString("yyyy-MM-dd"));

            Directory.CreateDirectory(videoFolder);

            var fileName =
                DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") +
                "_%03d.mp4";

            var outputFile = Path.Combine(
                videoFolder,
                fileName);

            Recorder.Start(
                settings,
                outputFile);

            State.SetState(Models.ApplicationState.Recording);
        }
        catch (Exception ex)
        {
            Logger.Write(
                $"Failed to start recording: {ex.Message}",
                Models.LogLevel.Error);

            Tray.SetStatus("Busy");
            State.SetState(Models.ApplicationState.Error);
        }
    }

    private void StopRecording()
    {
        _ = StopRecordingAsync();
    }

    private async Task StopRecordingAsync()
    {
        try
        {
            await Recorder.StopAsync();

            State.SetState(Models.ApplicationState.Ready);
        }
        catch (Exception ex)
        {
            Logger.Write(
                $"Failed to stop recording: {ex.Message}",
                Models.LogLevel.Error);

            State.SetState(Models.ApplicationState.Error);
        }
    }

    private void UpdateCameraStatus()
    {
        if (_isShuttingDown)
            return;

        if (Recorder.IsRecording)
        {
            Tray.SetStatus("Working");
            return;
        }

        var videoDevice = Config.Settings.VideoDevice;

        if (string.IsNullOrWhiteSpace(videoDevice))
        {
            Tray.SetStatus("Busy");
            return;
        }

        var devices = new DeviceService();

        var available = devices.IsVideoDeviceAvailable(videoDevice);

        Tray.SetStatus(available ? "Ready" : "Busy");
    }
    public void Shutdown()
    {
        if (_isShuttingDown)
            return;

        _isShuttingDown = true;

        if (Recorder.IsRecording)
        {
            StopRecording();
        }

        _cameraStatusTimer?.Dispose();
        _cameraStatusTimer = null;

        Logger.Write("Application stopped.");

        Tray.Dispose();

        System.Windows.Forms.Application.ExitThread();
    }
}