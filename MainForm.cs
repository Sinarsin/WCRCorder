using WCRCorder.Devices;
using WCRCorder.Models;
using WCRCorder.Services;

namespace WCRCorder
{
    public partial class MainForm : Form
    {
        private readonly ApplicationService _application;

        private readonly DeviceService _deviceService = new();
        public MainForm(ApplicationService application)
        {
            InitializeComponent();
            _application = application;
            LoadPasswordSettings();
            LoadGeneralSettings();
            LoadDevices();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                return;
            }

            base.OnFormClosing(e);
        }

        private void LoadPasswordSettings()
        {
            var password = _application.Config.Settings.Password;

            checkBoxPasswordEnabled.Checked = !string.IsNullOrEmpty(password);
            textBoxPassword.Text = password;
            textBoxPassword.Enabled = checkBoxPasswordEnabled.Checked;
        }

        private void LoadGeneralSettings()
        {
            var settings = _application.Config.Settings;

            textBoxOutputFolder.Text =
                settings.OutputFolder;

            numericSegmentMinutes.Value =
                settings.SegmentMinutes;

            checkBoxStartRecording.Checked =
                settings.StartRecording;

            checkBoxLogging.Checked =
                settings.Logging;

            textBoxBitrate.Text =
                settings.Bitrate;

            numericGOP.Value =
                Math.Clamp(settings.GOP, 1, 1000);

            numericForceKeyFrameSeconds.Value =
                Math.Clamp(settings.ForceKeyFrameSeconds, 1, 3600);

            checkBoxDrawTimestamp.Checked =
                settings.DrawTimestamp;

            checkBoxAutoFPS.Checked =
                settings.AutoFPS;

            comboBoxFPS.Enabled =
                !settings.AutoFPS;
        }

        private void checkBoxPasswordEnabled_CheckedChanged(
            object? sender,
            EventArgs e)
        {
            textBoxPassword.Enabled = checkBoxPasswordEnabled.Checked;

            if (!checkBoxPasswordEnabled.Checked)
            {
                textBoxPassword.Clear();
            }
        }
        private void buttonSave_Click(object? sender, EventArgs e)
        {
            if (checkBoxPasswordEnabled.Checked)
            {
                if (string.IsNullOrEmpty(textBoxPassword.Text))
                {
                    MessageBox.Show(
                        "Password cannot be empty.",
                        "Settings",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                _application.Config.Settings.Password = textBoxPassword.Text;
            }
            else
            {
                _application.Config.Settings.Password = string.Empty;
            }

            _application.Config.Settings.VideoDevice =
                comboBoxVideoDevice.SelectedItem?.ToString() ?? string.Empty;

            _application.Config.Settings.AudioDevice =
                comboBoxAudioDevice.SelectedItem?.ToString() ?? string.Empty;

            _application.Config.Settings.OutputFolder =
                textBoxOutputFolder.Text;

            _application.Config.Settings.SegmentMinutes =
                (int)numericSegmentMinutes.Value;

            _application.Config.Settings.StartRecording =
                checkBoxStartRecording.Checked;

            _application.Config.Settings.Logging =
                checkBoxLogging.Checked;
            _application.Config.Settings.Bitrate =
                textBoxBitrate.Text.Trim();

            _application.Config.Settings.GOP =
                (int)numericGOP.Value;

            _application.Config.Settings.ForceKeyFrameSeconds =
                (int)numericForceKeyFrameSeconds.Value;

            _application.Config.Settings.DrawTimestamp =
                checkBoxDrawTimestamp.Checked;

            _application.Config.Settings.AutoFPS =
                checkBoxAutoFPS.Checked;

            if (string.IsNullOrWhiteSpace(textBoxOutputFolder.Text))
            {
                MessageBox.Show(
                    "Output folder cannot be empty.",
                    "Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (comboBoxVideoDevice.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a video device.",
                    "Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (comboBoxResolution.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a video resolution.",
                    "Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (comboBoxResolution.SelectedItem is string resolution)
            {
                var parts = resolution
                    .Split('×', StringSplitOptions.TrimEntries);

                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out var width) &&
                    int.TryParse(parts[1], out var height))
                {
                    _application.Config.Settings.Width = width;
                    _application.Config.Settings.Height = height;
                }
            }

            if (!checkBoxAutoFPS.Checked)
            {
                if (comboBoxFPS.SelectedItem is not string fpsText ||
                    !int.TryParse(
                        fpsText.Replace("FPS", "").Trim(),
                        out var fps))
                {
                    MessageBox.Show(
                        "Please select a valid frame rate.",
                        "Settings",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                _application.Config.Settings.FPS = fps;
            }

            _application.Config.Save();

            Hide();
        }

        private void checkBoxAutoFPS_CheckedChanged(
            object? sender,
            EventArgs e)
        {
            comboBoxFPS.Enabled = !checkBoxAutoFPS.Checked;
        }

        private void LoadDevices()
        {
            comboBoxVideoDevice.Items.Clear();
            comboBoxAudioDevice.Items.Clear();

            var videoDevices = _deviceService.GetVideoDevices();
            var audioDevices = _deviceService.GetAudioDevices();

            foreach (var device in videoDevices)
            {
                comboBoxVideoDevice.Items.Add(device);
            }

            foreach (var device in audioDevices)
            {
                comboBoxAudioDevice.Items.Add(device);
            }

            SelectSavedDevice(
                comboBoxVideoDevice,
                _application.Config.Settings.VideoDevice);

            SelectSavedDevice(
                comboBoxAudioDevice,
                _application.Config.Settings.AudioDevice);

            if (comboBoxVideoDevice.SelectedItem is string videoDevice)
            {
                LoadVideoFormats(videoDevice);
            }
        }

        private void LoadVideoFormats(string deviceName)
        {
            _videoFormats = _deviceService.GetVideoFormats(deviceName);

            comboBoxResolution.Items.Clear();
            comboBoxFPS.Items.Clear();

            var resolutions = _videoFormats
                .Select(x => $"{x.Width} × {x.Height}")
                .Distinct()
                .ToList();

            foreach (var resolution in resolutions)
            {
                comboBoxResolution.Items.Add(resolution);
            }

            var savedWidth = _application.Config.Settings.Width;
            var savedHeight = _application.Config.Settings.Height;

            var savedResolution =
                $"{savedWidth} × {savedHeight}";

            var savedResolutionIndex =
                comboBoxResolution.Items.IndexOf(savedResolution);

            if (savedResolutionIndex >= 0)
            {
                comboBoxResolution.SelectedIndex = savedResolutionIndex;
            }
            else if (comboBoxResolution.Items.Count > 0)
            {
                comboBoxResolution.SelectedIndex = 0;
            }

            UpdateFPSList();
        }

        private void UpdateFPSList()
        {
            comboBoxFPS.Items.Clear();

            if (comboBoxResolution.SelectedItem is not string resolution)
                return;

            var parts = resolution
                .Split('×', StringSplitOptions.TrimEntries);

            if (parts.Length != 2)
                return;

            if (!int.TryParse(parts[0], out var width))
                return;

            if (!int.TryParse(parts[1], out var height))
                return;

            var formats = _videoFormats
                .Where(x =>
                    x.Width == width &&
                    x.Height == height)
                .ToList();

            var fpsValues = new SortedSet<double>();

            foreach (var format in formats)
            {
                var min = (int)Math.Ceiling(format.MinFPS);
                var max = (int)Math.Floor(format.MaxFPS);

                for (var fps = min; fps <= max; fps++)
                {
                    fpsValues.Add(fps);
                }
            }

            foreach (var fps in fpsValues)
            {
                comboBoxFPS.Items.Add($"{fps} FPS");
            }

            var savedFPS = _application.Config.Settings.FPS;

            var savedFPSIndex =
                comboBoxFPS.Items.IndexOf($"{savedFPS} FPS");

            if (savedFPSIndex >= 0)
            {
                comboBoxFPS.SelectedIndex = savedFPSIndex;
            }
            else if (comboBoxFPS.Items.Count > 0)
            {
                comboBoxFPS.SelectedIndex =
                    comboBoxFPS.Items.Count - 1;
            }
        }

        private static void SelectSavedDevice(
            ComboBox comboBox,
            string savedDevice)
        {
            if (string.IsNullOrWhiteSpace(savedDevice))
            {
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }

                return;
            }

            var index = comboBox.Items.IndexOf(savedDevice);

            if (index >= 0)
            {
                comboBox.SelectedIndex = index;
            }
            else if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private IReadOnlyList<VideoFormat> _videoFormats =
            Array.Empty<VideoFormat>();

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFPSList();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();

            dialog.Description = "Select output folder";

            dialog.SelectedPath = textBoxOutputFolder.Text;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxOutputFolder.Text = dialog.SelectedPath;
            }
        }
    }

}
