namespace PianoEnhancer
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
                _piano.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.InputDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.OutputDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.InputDeviceLabel = new System.Windows.Forms.Label();
            this.OutputDeviceLabel = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SetupBox = new System.Windows.Forms.GroupBox();
            this.DeviceControlGroup = new System.Windows.Forms.GroupBox();
            this.SetVoiceButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.RecordButton = new System.Windows.Forms.Button();
            this.RecordingBox = new System.Windows.Forms.GroupBox();
            this.NextButton = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.LoadRecordingDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveRecordingDialog = new System.Windows.Forms.SaveFileDialog();
            this.DrawTimer = new System.Windows.Forms.Timer(this.components);
            this.SetupBox.SuspendLayout();
            this.DeviceControlGroup.SuspendLayout();
            this.RecordingBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputDeviceComboBox
            // 
            this.InputDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputDeviceComboBox.FormattingEnabled = true;
            this.InputDeviceComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.InputDeviceComboBox.Location = new System.Drawing.Point(123, 24);
            this.InputDeviceComboBox.Name = "InputDeviceComboBox";
            this.InputDeviceComboBox.Size = new System.Drawing.Size(182, 33);
            this.InputDeviceComboBox.TabIndex = 0;
            // 
            // OutputDeviceComboBox
            // 
            this.OutputDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OutputDeviceComboBox.FormattingEnabled = true;
            this.OutputDeviceComboBox.Location = new System.Drawing.Point(443, 24);
            this.OutputDeviceComboBox.Name = "OutputDeviceComboBox";
            this.OutputDeviceComboBox.Size = new System.Drawing.Size(182, 33);
            this.OutputDeviceComboBox.TabIndex = 1;
            // 
            // InputDeviceLabel
            // 
            this.InputDeviceLabel.AutoSize = true;
            this.InputDeviceLabel.Location = new System.Drawing.Point(6, 27);
            this.InputDeviceLabel.Name = "InputDeviceLabel";
            this.InputDeviceLabel.Size = new System.Drawing.Size(111, 25);
            this.InputDeviceLabel.TabIndex = 2;
            this.InputDeviceLabel.Text = "Input Device";
            // 
            // OutputDeviceLabel
            // 
            this.OutputDeviceLabel.AutoSize = true;
            this.OutputDeviceLabel.Location = new System.Drawing.Point(311, 27);
            this.OutputDeviceLabel.Name = "OutputDeviceLabel";
            this.OutputDeviceLabel.Size = new System.Drawing.Size(126, 25);
            this.OutputDeviceLabel.TabIndex = 3;
            this.OutputDeviceLabel.Text = "Output Device";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(631, 24);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(112, 33);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(749, 24);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(112, 33);
            this.RefreshButton.TabIndex = 5;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButtonClick);
            // 
            // SetupBox
            // 
            this.SetupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SetupBox.Controls.Add(this.InputDeviceLabel);
            this.SetupBox.Controls.Add(this.RefreshButton);
            this.SetupBox.Controls.Add(this.InputDeviceComboBox);
            this.SetupBox.Controls.Add(this.ConnectButton);
            this.SetupBox.Controls.Add(this.OutputDeviceComboBox);
            this.SetupBox.Controls.Add(this.OutputDeviceLabel);
            this.SetupBox.Location = new System.Drawing.Point(12, 12);
            this.SetupBox.Name = "SetupBox";
            this.SetupBox.Size = new System.Drawing.Size(884, 71);
            this.SetupBox.TabIndex = 6;
            this.SetupBox.TabStop = false;
            this.SetupBox.Text = "Setup";
            // 
            // DeviceControlGroup
            // 
            this.DeviceControlGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceControlGroup.Controls.Add(this.SetVoiceButton);
            this.DeviceControlGroup.Controls.Add(this.NewButton);
            this.DeviceControlGroup.Controls.Add(this.LoadButton);
            this.DeviceControlGroup.Controls.Add(this.SaveButton);
            this.DeviceControlGroup.Controls.Add(this.PlayButton);
            this.DeviceControlGroup.Controls.Add(this.StopButton);
            this.DeviceControlGroup.Controls.Add(this.RecordButton);
            this.DeviceControlGroup.Location = new System.Drawing.Point(12, 90);
            this.DeviceControlGroup.Name = "DeviceControlGroup";
            this.DeviceControlGroup.Size = new System.Drawing.Size(884, 121);
            this.DeviceControlGroup.TabIndex = 7;
            this.DeviceControlGroup.TabStop = false;
            this.DeviceControlGroup.Text = "Device Control";
            // 
            // SetVoiceButton
            // 
            this.SetVoiceButton.Location = new System.Drawing.Point(7, 72);
            this.SetVoiceButton.Name = "SetVoiceButton";
            this.SetVoiceButton.Size = new System.Drawing.Size(861, 34);
            this.SetVoiceButton.TabIndex = 4;
            this.SetVoiceButton.Text = "Set Voice";
            this.SetVoiceButton.UseVisualStyleBackColor = true;
            this.SetVoiceButton.Click += new System.EventHandler(this.SetVoiceButtonClick);
            // 
            // NewButton
            // 
            this.NewButton.Location = new System.Drawing.Point(704, 31);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(164, 34);
            this.NewButton.TabIndex = 3;
            this.NewButton.Text = "New Recording";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButtonClick);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(534, 31);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(164, 34);
            this.LoadButton.TabIndex = 3;
            this.LoadButton.Text = "Load Recording";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(364, 31);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(164, 34);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save Recording";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(245, 31);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(112, 34);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButtonClick);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(126, 31);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(112, 34);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // RecordButton
            // 
            this.RecordButton.Location = new System.Drawing.Point(7, 31);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(112, 34);
            this.RecordButton.TabIndex = 0;
            this.RecordButton.Text = "Record";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.RecordButtonClick);
            // 
            // RecordingBox
            // 
            this.RecordingBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RecordingBox.Controls.Add(this.NextButton);
            this.RecordingBox.Controls.Add(this.PrevButton);
            this.RecordingBox.Location = new System.Drawing.Point(12, 217);
            this.RecordingBox.Name = "RecordingBox";
            this.RecordingBox.Size = new System.Drawing.Size(883, 242);
            this.RecordingBox.TabIndex = 8;
            this.RecordingBox.TabStop = false;
            this.RecordingBox.Text = "Recording";
            // 
            // NextButton
            // 
            this.NextButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.NextButton.Location = new System.Drawing.Point(443, 201);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(112, 34);
            this.NextButton.TabIndex = 2;
            this.NextButton.Text = ">>";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButtonClick);
            // 
            // PrevButton
            // 
            this.PrevButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PrevButton.Location = new System.Drawing.Point(324, 201);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(112, 34);
            this.PrevButton.TabIndex = 1;
            this.PrevButton.Text = "<<";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevButtonClick);
            // 
            // LoadRecordingDialog
            // 
            this.LoadRecordingDialog.AddExtension = false;
            this.LoadRecordingDialog.FileName = "LoadRecordingDialog";
            this.LoadRecordingDialog.Filter = "Recordings|*.mid";
            this.LoadRecordingDialog.InitialDirectory = "%USERPROFILE%/Documents";
            // 
            // SaveRecordingDialog
            // 
            this.SaveRecordingDialog.AddExtension = false;
            this.SaveRecordingDialog.Filter = "Recordings|*.mid";
            this.SaveRecordingDialog.InitialDirectory = "%USERPROFILE%/Documents";
            // 
            // DrawTimer
            // 
            this.DrawTimer.Tick += new System.EventHandler(this.DrawTimerTick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 470);
            this.Controls.Add(this.RecordingBox);
            this.Controls.Add(this.DeviceControlGroup);
            this.Controls.Add(this.SetupBox);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(930, 500);
            this.Name = "MainForm";
            this.Text = "PianoEnhancer";
            this.SetupBox.ResumeLayout(false);
            this.SetupBox.PerformLayout();
            this.DeviceControlGroup.ResumeLayout(false);
            this.RecordingBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox InputDeviceComboBox;
        private System.Windows.Forms.ComboBox OutputDeviceComboBox;
        private System.Windows.Forms.Label InputDeviceLabel;
        private System.Windows.Forms.Label OutputDeviceLabel;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.GroupBox SetupBox;
        private System.Windows.Forms.GroupBox DeviceControlGroup;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button RecordButton;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Button SetVoiceButton;
        private System.Windows.Forms.GroupBox RecordingBox;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.Panel RecordingPanel;
        private System.Windows.Forms.OpenFileDialog LoadRecordingDialog;
        private System.Windows.Forms.SaveFileDialog SaveRecordingDialog;
        private System.Windows.Forms.Timer DrawTimer;
    }
}

