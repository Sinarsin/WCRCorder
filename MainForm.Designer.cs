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
            buttonSave.Location = new Point(12, 250);
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}
