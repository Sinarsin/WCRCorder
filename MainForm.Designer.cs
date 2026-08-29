namespace WCRCorder
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelPassword = new Label();
            textBoxPassword = new TextBox();
            checkBoxPasswordEnabled = new CheckBox();
            buttonSave = new Button();
            comboBoxAudioDevice = new ComboBox();
            comboBoxVideoDevice = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            comboBoxFPS = new ComboBox();
            comboBoxResolution = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            textBoxOutputFolder = new TextBox();
            label5 = new Label();
            buttonBrowse = new Button();
            label6 = new Label();
            numericSegmentMinutes = new NumericUpDown();
            checkBoxStartRecording = new CheckBox();
            checkBoxLogging = new CheckBox();
            labelBitrate = new Label();
            textBoxBitrate = new TextBox();
            labelGOP = new Label();
            numericGOP = new NumericUpDown();
            labelForceKeyFrameSeconds = new Label();
            numericForceKeyFrameSeconds = new NumericUpDown();
            checkBoxDrawTimestamp = new CheckBox();
            checkBoxAutoFPS = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numericSegmentMinutes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericGOP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericForceKeyFrameSeconds).BeginInit();
            SuspendLayout();
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(61, 39);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(60, 15);
            labelPassword.TabIndex = 0;
            labelPassword.Text = "Password:";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Enabled = false;
            textBoxPassword.Location = new Point(127, 36);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(100, 23);
            textBoxPassword.TabIndex = 1;
            textBoxPassword.UseSystemPasswordChar = true;
            textBoxPassword.TextChanged += textBoxPassword_TextChanged;
            // 
            // checkBoxPasswordEnabled
            // 
            checkBoxPasswordEnabled.AutoSize = true;
            checkBoxPasswordEnabled.Location = new Point(12, 12);
            checkBoxPasswordEnabled.Name = "checkBoxPasswordEnabled";
            checkBoxPasswordEnabled.Size = new Size(109, 19);
            checkBoxPasswordEnabled.TabIndex = 2;
            checkBoxPasswordEnabled.Text = "Protect Settings";
            checkBoxPasswordEnabled.UseVisualStyleBackColor = true;
            checkBoxPasswordEnabled.CheckedChanged += checkBoxPasswordEnabled_CheckedChanged;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(311, 8);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // comboBoxAudioDevice
            // 
            comboBoxAudioDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAudioDevice.FormattingEnabled = true;
            comboBoxAudioDevice.Location = new Point(95, 93);
            comboBoxAudioDevice.Name = "comboBoxAudioDevice";
            comboBoxAudioDevice.Size = new Size(215, 23);
            comboBoxAudioDevice.TabIndex = 4;
            // 
            // comboBoxVideoDevice
            // 
            comboBoxVideoDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxVideoDevice.FormattingEnabled = true;
            comboBoxVideoDevice.Location = new Point(95, 67);
            comboBoxVideoDevice.Name = "comboBoxVideoDevice";
            comboBoxVideoDevice.Size = new Size(215, 23);
            comboBoxVideoDevice.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 70);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 6;
            label1.Text = "Video device:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 96);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 7;
            label2.Text = "Audio device:";
            // 
            // comboBoxFPS
            // 
            comboBoxFPS.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFPS.FormattingEnabled = true;
            comboBoxFPS.Location = new Point(95, 161);
            comboBoxFPS.Name = "comboBoxFPS";
            comboBoxFPS.Size = new Size(121, 23);
            comboBoxFPS.TabIndex = 8;
            // 
            // comboBoxResolution
            // 
            comboBoxResolution.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxResolution.FormattingEnabled = true;
            comboBoxResolution.Location = new Point(95, 132);
            comboBoxResolution.Name = "comboBoxResolution";
            comboBoxResolution.Size = new Size(121, 23);
            comboBoxResolution.TabIndex = 9;
            comboBoxResolution.SelectedIndexChanged += comboBoxResolution_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 135);
            label3.Name = "label3";
            label3.Size = new Size(66, 15);
            label3.TabIndex = 10;
            label3.Text = "Resolution:\n";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 164);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 11;
            label4.Text = "Frame rate:";
            // 
            // textBoxOutputFolder
            // 
            textBoxOutputFolder.Location = new Point(95, 190);
            textBoxOutputFolder.Name = "textBoxOutputFolder";
            textBoxOutputFolder.Size = new Size(186, 23);
            textBoxOutputFolder.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 193);
            label5.Name = "label5";
            label5.Size = new Size(82, 15);
            label5.TabIndex = 13;
            label5.Text = "Output folder:";
            label5.Click += label5_Click;
            // 
            // buttonBrowse
            // 
            buttonBrowse.Location = new Point(287, 190);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(75, 23);
            buttonBrowse.TabIndex = 14;
            buttonBrowse.Text = "Browse";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 218);
            label6.Name = "label6";
            label6.Size = new Size(148, 15);
            label6.TabIndex = 15;
            label6.Text = "Segment length (minutes):";
            // 
            // numericSegmentMinutes
            // 
            numericSegmentMinutes.Location = new Point(166, 216);
            numericSegmentMinutes.Maximum = new decimal(new int[] { 720, 0, 0, 0 });
            numericSegmentMinutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericSegmentMinutes.Name = "numericSegmentMinutes";
            numericSegmentMinutes.Size = new Size(120, 23);
            numericSegmentMinutes.TabIndex = 16;
            numericSegmentMinutes.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // checkBoxStartRecording
            // 
            checkBoxStartRecording.AutoSize = true;
            checkBoxStartRecording.Location = new Point(12, 247);
            checkBoxStartRecording.Name = "checkBoxStartRecording";
            checkBoxStartRecording.Size = new Size(179, 19);
            checkBoxStartRecording.TabIndex = 17;
            checkBoxStartRecording.Text = "Start recording automatically";
            checkBoxStartRecording.UseVisualStyleBackColor = true;
            // 
            // checkBoxLogging
            // 
            checkBoxLogging.AutoSize = true;
            checkBoxLogging.Location = new Point(11, 272);
            checkBoxLogging.Name = "checkBoxLogging";
            checkBoxLogging.Size = new Size(105, 19);
            checkBoxLogging.TabIndex = 18;
            checkBoxLogging.Text = "Enable logging";
            checkBoxLogging.UseVisualStyleBackColor = true;
            // 
            // labelBitrate
            // 
            labelBitrate.AutoSize = true;
            labelBitrate.Location = new Point(12, 301);
            labelBitrate.Name = "labelBitrate";
            labelBitrate.Size = new Size(44, 15);
            labelBitrate.TabIndex = 19;
            labelBitrate.Text = "Bitrate:";
            // 
            // textBoxBitrate
            // 
            textBoxBitrate.Location = new Point(95, 298);
            textBoxBitrate.Name = "textBoxBitrate";
            textBoxBitrate.Size = new Size(100, 23);
            textBoxBitrate.TabIndex = 20;
            // 
            // labelGOP
            // 
            labelGOP.AutoSize = true;
            labelGOP.Location = new Point(12, 330);
            labelGOP.Name = "labelGOP";
            labelGOP.Size = new Size(34, 15);
            labelGOP.TabIndex = 21;
            labelGOP.Text = "GOP:";
            // 
            // numericGOP
            // 
            numericGOP.Location = new Point(95, 327);
            numericGOP.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericGOP.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericGOP.Name = "numericGOP";
            numericGOP.Size = new Size(100, 23);
            numericGOP.TabIndex = 22;
            numericGOP.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelForceKeyFrameSeconds
            // 
            labelForceKeyFrameSeconds.AutoSize = true;
            labelForceKeyFrameSeconds.Location = new Point(12, 359);
            labelForceKeyFrameSeconds.Name = "labelForceKeyFrameSeconds";
            labelForceKeyFrameSeconds.Size = new Size(133, 15);
            labelForceKeyFrameSeconds.TabIndex = 23;
            labelForceKeyFrameSeconds.Text = "Key frame interval (sec):";
            // 
            // numericForceKeyFrameSeconds
            // 
            numericForceKeyFrameSeconds.Location = new Point(166, 356);
            numericForceKeyFrameSeconds.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numericForceKeyFrameSeconds.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericForceKeyFrameSeconds.Name = "numericForceKeyFrameSeconds";
            numericForceKeyFrameSeconds.Size = new Size(100, 23);
            numericForceKeyFrameSeconds.TabIndex = 24;
            numericForceKeyFrameSeconds.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // checkBoxDrawTimestamp
            // 
            checkBoxDrawTimestamp.AutoSize = true;
            checkBoxDrawTimestamp.Location = new Point(12, 385);
            checkBoxDrawTimestamp.Name = "checkBoxDrawTimestamp";
            checkBoxDrawTimestamp.Size = new Size(180, 19);
            checkBoxDrawTimestamp.TabIndex = 25;
            checkBoxDrawTimestamp.Text = "Show date and time on video";
            checkBoxDrawTimestamp.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoFPS
            // 
            checkBoxAutoFPS.AutoSize = true;
            checkBoxAutoFPS.Location = new Point(225, 164);
            checkBoxAutoFPS.Name = "checkBoxAutoFPS";
            checkBoxAutoFPS.Size = new Size(52, 19);
            checkBoxAutoFPS.TabIndex = 26;
            checkBoxAutoFPS.Text = "Auto";
            checkBoxAutoFPS.UseVisualStyleBackColor = true;
            checkBoxAutoFPS.CheckedChanged += checkBoxAutoFPS_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBoxAutoFPS);
            Controls.Add(checkBoxDrawTimestamp);
            Controls.Add(numericForceKeyFrameSeconds);
            Controls.Add(labelForceKeyFrameSeconds);
            Controls.Add(numericGOP);
            Controls.Add(labelGOP);
            Controls.Add(textBoxBitrate);
            Controls.Add(labelBitrate);
            Controls.Add(checkBoxLogging);
            Controls.Add(checkBoxStartRecording);
            Controls.Add(numericSegmentMinutes);
            Controls.Add(label6);
            Controls.Add(buttonBrowse);
            Controls.Add(label5);
            Controls.Add(textBoxOutputFolder);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(comboBoxResolution);
            Controls.Add(comboBoxFPS);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBoxVideoDevice);
            Controls.Add(comboBoxAudioDevice);
            Controls.Add(buttonSave);
            Controls.Add(checkBoxPasswordEnabled);
            Controls.Add(textBoxPassword);
            Controls.Add(labelPassword);
            Name = "MainForm";
            Text = "Settings";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)numericSegmentMinutes).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericGOP).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericForceKeyFrameSeconds).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelPassword;
        private TextBox textBoxPassword;
        private CheckBox checkBoxPasswordEnabled;
        private Button buttonSave;
        private ComboBox comboBoxAudioDevice;
        private ComboBox comboBoxVideoDevice;
        private Label label1;
        private Label label2;
        private ComboBox comboBoxFPS;
        private ComboBox comboBoxResolution;
        private Label label3;
        private Label label4;
        private TextBox textBoxOutputFolder;
        private Label label5;
        private Button buttonBrowse;
        private Label label6;
        private NumericUpDown numericSegmentMinutes;
        private CheckBox checkBoxStartRecording;
        private CheckBox checkBoxLogging;
        private Label labelBitrate;
        private TextBox textBoxBitrate;
        private Label labelGOP;
        private NumericUpDown numericGOP;
        private Label labelForceKeyFrameSeconds;
        private NumericUpDown numericForceKeyFrameSeconds;
        private CheckBox checkBoxDrawTimestamp;
        private CheckBox checkBoxAutoFPS;
    }
}
