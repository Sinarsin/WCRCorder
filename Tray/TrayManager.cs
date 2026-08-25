using System.Diagnostics;
using System.Windows.Forms;
using WCRCorder.Utils;

namespace WCRCorder.Tray;

public sealed class TrayManager : IDisposable
{
    private readonly NotifyIcon _notifyIcon;
    private readonly ContextMenuStrip _menu;

    public event Action? StartRequested;
    public event Action? StopRequested;
    public event Action? SettingsRequested;
    public event Action? ExitRequested;

    public TrayManager()
    {
        _menu = new ContextMenuStrip();

        _menu.Items.Add("Start", null, (_, _) => StartRequested?.Invoke());
        _menu.Items.Add("Stop", null, (_, _) => StopRequested?.Invoke());

        _menu.Items.Add(new ToolStripSeparator());

        _menu.Items.Add("Settings...", null, (_, _) => SettingsRequested?.Invoke());

        _menu.Items.Add("Open Data Folder", null, (_, _) =>
        {
            Process.Start("explorer.exe", AppPaths.DataDirectory);
        });

        _menu.Items.Add(new ToolStripSeparator());

        _menu.Items.Add("Exit", null, (_, _) => ExitRequested?.Invoke());

        _notifyIcon = new NotifyIcon
        {
            Icon = SystemIcons.Application,
            Visible = true,
            Text = "WCRCorder",
            ContextMenuStrip = _menu
        };
    }

    public void Dispose()
    {
        _notifyIcon.Visible = false;
        _notifyIcon.Dispose();
        _menu.Dispose();
    }
}