using System;
using System.Drawing;
using System.Windows.Forms;

namespace PianoEnhancer
{
    public partial class MainForm : Form
    {

        public sealed class DrawPanel : Panel
        {
            public DrawPanel()
            {
                DoubleBuffered = true;
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                BackColor = Color.White;
            }
        }

        private enum PianoState
        {
            Idle, Recording, Playing
        }

        private readonly PianoController _piano;
        private bool _connected;
        private PianoState _state;
        private readonly RecordingDrawer _drawer;

        private PianoState State
        {
            get => _state;
            set { _state = value; RefreshPianoState(); }
        }

        public MainForm()
        {
            InitializeComponent();
            RecordingPanel = new DrawPanel
            {
                Location = new Point(10, 30), Name = "RecordingPanel", Size = new Size(860, 160), TabIndex = 0
            };
            RecordingPanel.MouseClick += RecordingPanelClick;
            RecordingPanel.Paint += RecordingPanelPaint;
            RecordingBox.Controls.Add(RecordingPanel);
            _piano = new PianoController();
            _piano.PlayBack.PlaybackStopped += () => State = PianoState.Idle;
            _connected = false;
            State = PianoState.Idle;
            EnumerateDevices();
            _drawer = new RecordingDrawer {Recorder = _piano.Recorder, PlayBack = _piano.PlayBack};
            DrawTimer.Start();
        }

        private void RecordingPanelClick(object sender, MouseEventArgs e)
        {
            _drawer.HandleClick(e.X,e.Y,RecordingPanel.Size, e.Button);
        }

        private void RefreshConnectionState()
        {
            ConnectButton.Enabled = InputDeviceComboBox.SelectedIndex >= 0 && OutputDeviceComboBox.SelectedIndex >= 0;
            DeviceControlGroup.Enabled = _connected;
            RecordingBox.Enabled = _connected;
        }

        private void RefreshPianoState()
        {
            //invoke on the ui thread specifically because it might be called from other threads
            if (RecordButton.InvokeRequired)
            {
                RecordButton.Invoke((Action) delegate
                {
                    RecordButton.Enabled = _state == PianoState.Idle;
                    PlayButton.Enabled = _state == PianoState.Idle;
                    StopButton.Enabled = _state != PianoState.Idle;
                    SaveButton.Enabled = _state == PianoState.Idle;
                    LoadButton.Enabled = _state == PianoState.Idle;
                    NewButton.Enabled = _state == PianoState.Idle;
                });
            }
            else
            {
                RecordButton.Enabled = _state == PianoState.Idle;
                PlayButton.Enabled = _state == PianoState.Idle;
                StopButton.Enabled = _state != PianoState.Idle;
                SaveButton.Enabled = _state == PianoState.Idle;
                LoadButton.Enabled = _state == PianoState.Idle;
                NewButton.Enabled = _state == PianoState.Idle;
            }
        }

        private void EnumerateDevices()
        {
            InputDeviceComboBox.Items.Clear();
            OutputDeviceComboBox.Items.Clear();

            using var tempLabel = new Label();
            var longestInDevice = InputDeviceComboBox.Width;
            var longestOutDevice = OutputDeviceComboBox.Width;

            foreach (var device in PianoController.GetConnectedInputDevices())
            {
                var name = $"{device.ProductName} - {device.Manufacturer}";
                InputDeviceComboBox.Items.Add(name);
                tempLabel.Text = name;
                longestInDevice = Math.Max(longestInDevice, tempLabel.PreferredWidth);
            }

            InputDeviceComboBox.DropDownWidth = longestInDevice;
            if(InputDeviceComboBox.Items.Count > 0) InputDeviceComboBox.SelectedIndex = 0;

            foreach (var device in PianoController.GetConnectedOutDevices())
            {
                var name = $"{device.ProductName} - {device.Manufacturer}";
                OutputDeviceComboBox.Items.Add(name);
                tempLabel.Text = name;
                longestOutDevice = Math.Max(longestOutDevice, tempLabel.PreferredWidth);
            }

            OutputDeviceComboBox.DropDownWidth = longestOutDevice;
            if(OutputDeviceComboBox.Items.Count > 0) OutputDeviceComboBox.SelectedIndex = 0;
            RefreshConnectionState();
        }

        private void SetVoiceButtonClick(object sender, EventArgs e)
        {
            using var voiceSelector = new VoiceSelector();
            if (voiceSelector.ShowDialog() == DialogResult.OK)
            {
                _piano.SetVoice(voiceSelector.Voice.ToVoiceComposition());
            }
        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            _piano.Init(InputDeviceComboBox.SelectedIndex,OutputDeviceComboBox.SelectedIndex);
            _connected = true;
            RefreshConnectionState();
        }

        private void RefreshButtonClick(object sender, EventArgs e)
        {
            EnumerateDevices();
        }

        private void RecordButtonClick(object sender, EventArgs e)
        { 
            _piano.Recorder.StartRecording();
            State = PianoState.Recording;
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            switch (State)
            {
                case PianoState.Recording:
                    _piano.Recorder.StopRecording();
                    break;
                case PianoState.Playing:
                    _piano.Stop();
                    break;
                case PianoState.Idle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            State = PianoState.Idle;
        }

        private void PlayButtonClick(object sender, EventArgs e)
        {
            _piano.Play();
            State = PianoState.Playing;
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            if (SaveRecordingDialog.ShowDialog() != DialogResult.OK) return;
            _piano.Recorder.SaveRecording(SaveRecordingDialog.FileName);
        }

        private void LoadButtonClick(object sender, EventArgs e)
        {
            if (LoadRecordingDialog.ShowDialog() != DialogResult.OK) return;
            _piano.Recorder.LoadRecording(LoadRecordingDialog.FileName);
        }

        private void NewButtonClick(object sender, EventArgs e)
        {
            _piano.Recorder.CreateNewRecording();
        }

        private void DrawTimerTick(object sender, EventArgs e)
        {
            if(_connected) RecordingPanel.Refresh();
        }

        private void RecordingPanelPaint(object sender, PaintEventArgs e)
        {
            _drawer.DrawPanel(e.Graphics, RecordingPanel.Size);
            SetPrevNextButtonsEnabled();
        }

        private void SetPrevNextButtonsEnabled()
        {
            PrevButton.Enabled = _drawer.CurrentPage > 0;
            NextButton.Enabled = _drawer.CurrentPage < _drawer.PageCount - 1;
        }

        private void PrevButtonClick(object sender, EventArgs e)
        {
            _drawer.CurrentPage--;
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            _drawer.CurrentPage++;
        }
    }
}
