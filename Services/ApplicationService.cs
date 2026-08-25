using WCRCorder;
using WCRCorder.Config;
using WCRCorder.Devices;
using WCRCorder.Logging;
using WCRCorder.Tray;
using WCRCorder.Utils;

namespace WCRCorder.Services;

public sealed class ApplicationService
{
    private MainForm? _mainForm;
    public TrayManager Tray { get; }
    public ConfigService Config { get; }
    public LogService Logger { get; }
    public ApplicationStateService State { get; }

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
        Tray = new TrayManager();
        Tray.SettingsRequested += ShowSettings;
        Tray.ExitRequested += Shutdown;
    }

    public void Initialize()
    {
        AppPaths.CreateDirectories();

        Config.Load();

        Logger.Write("Application started. Initialize completed.");

        State.SetState(Models.ApplicationState.Ready);

        //временно
        var devices = new DeviceService();

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
    public void Shutdown()
    {
        Logger.Write("Application stopped.");

        Tray.Dispose();

        System.Windows.Forms.Application.ExitThread();
    }
}