using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace PianoEnhancer
{
    public partial class VoiceSelector : Form
    {
        private const int DropdownTopOffset = 100;
        private const int DropdownLeftOffset = 10;
        private const int DropdownHeight = 33;
        private const int DropdownVerticalSpace = 40;
        private readonly string _presetPath = Environment.ExpandEnvironmentVariables("%APPDATA%\\PianoEnhancer\\");
        private readonly BindingSource[] _voiceBindingSources;
        private readonly BindingSource[] _allocationBindingSources;
        private readonly BindingSource _splitBindingSource;
        private readonly NumericUpDown[] _transpositionUpDowns;
        internal VoicePreset Voice { get; private set; }

        public VoiceSelector()
        {
            InitializeComponent();
            _voiceBindingSources = new BindingSource[Configuration.TotalChannels];
            _allocationBindingSources = new BindingSource[Configuration.TotalChannels];
            _transpositionUpDowns = new NumericUpDown[Configuration.TotalChannels];
            _splitBindingSource = new BindingSource{DataSource = Voices.NoteNames};

            SplitPointComboBox.DataSource = _splitBindingSource;

            for(var i = 0; i < Configuration.TotalChannels; ++i)
            {
                _voiceBindingSources[i] = new BindingSource {DataSource = Voices.BaseVoices};
                var voiceComboBox = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(DropdownLeftOffset, i * DropdownVerticalSpace + DropdownTopOffset),
                    Size = new Size(170,DropdownHeight),
                    DataSource = _voiceBindingSources[i], DisplayMember = "DisplayName",
                };
                Controls.Add(voiceComboBox);

                _allocationBindingSources[i] = new BindingSource{DataSource = Voices.ChannelSides};
                var allocationComboBox = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(DropdownLeftOffset + 180, i*DropdownVerticalSpace + DropdownTopOffset),
                    Size = new Size(80,DropdownHeight),
                    DataSource = _allocationBindingSources[i]
                };
                Controls.Add(allocationComboBox);

                var transpositionBox = new NumericUpDown
                {
                    Minimum = -3*9, //three octaves
                    Maximum = 3*9,
                    Increment = 1,
                    Location = new Point(DropdownLeftOffset + 270, i * DropdownVerticalSpace + DropdownTopOffset),
                    Size = new Size(80,DropdownHeight)
                };
                Controls.Add(transpositionBox);
                _transpositionUpDowns[i] = transpositionBox;
            }
            RefreshPresetList();
        }

        private void RefreshPresetList()
        {
            PresetDropDown.Items.Clear();
            if (!Directory.Exists(_presetPath)) Directory.CreateDirectory(_presetPath);
            var presets = Directory.EnumerateFiles(_presetPath);
            foreach (var preset in presets)
            {
                PresetDropDown.Items.Add(preset.Substring(preset.LastIndexOf('\\') + 1));
            }
        }

        private void ApplyButtonClick(object sender, EventArgs e)
        {
            SetVoice();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SetVoice()
        {
            var channelVoice = new BaseVoice[Configuration.TotalChannels];
            var channelAllocation = new VoiceComposition.ChannelSide[Configuration.TotalChannels];
            var channelTransposition = new int[Configuration.TotalChannels];

            for (var i = 0; i < Configuration.TotalChannels; ++i)
            {
                channelVoice[i] = _voiceBindingSources[i].Current as BaseVoice? ?? new BaseVoice();
                channelAllocation[i] = (VoiceComposition.ChannelSide) _allocationBindingSources[i].Position;
                channelTransposition[i] = (int)_transpositionUpDowns[i].Value;
            }

            Voice = new VoicePreset(PresetDropDown.Text,_splitBindingSource.Position,channelVoice,channelAllocation,channelTransposition);
        }

        private void GetVoice()
        {
            for (var i = 0; i < Configuration.TotalChannels; ++i)
            {
                _transpositionUpDowns[i].Value = Voice.Transposition[i];
                _allocationBindingSources[i].Position = _allocationBindingSources[i].IndexOf(Voice.Allocation[i]);
                _voiceBindingSources[i].Position = _voiceBindingSources[i].IndexOf(Voice.Voice[i]);
            }

            _splitBindingSource.Position = Voice.SplitPoint;
        }

        private void PresetSaveButtonClick(object sender, EventArgs e)
        {
            SetVoice();
            using var outFile = new StreamWriter(_presetPath + PresetDropDown.Text);
            outFile.Write(JsonSerializer.Serialize(Voice));
            outFile.Close();
            RefreshPresetList();
        }

        private void PresetDeleteButtonClick(object sender, EventArgs e)
        {
            if (PresetDropDown.SelectedIndex == -1) return;
            File.Delete(_presetPath + PresetDropDown.Text);
            RefreshPresetList();
        }

        private void PresetDropDownSelectedIndexChanged(object sender, EventArgs e)
        {
            if (PresetDropDown.SelectedIndex == -1) return;
            using var inFile = new StreamReader(_presetPath + PresetDropDown.Text);
            Voice = JsonSerializer.Deserialize<VoicePreset>(inFile.ReadToEnd());
            GetVoice();
        }
    }
}
