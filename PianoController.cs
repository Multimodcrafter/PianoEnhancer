using System;
using System.Collections.Generic;
using NAudio.Midi;

namespace PianoEnhancer
{
    internal class PianoController : IDisposable
    {
        private MidiIn _inDevice;
        private MidiOut _outDevice;
        private bool _initialized;
        private readonly Dictionary<int,HashSet<KeyValuePair<int,int>>> _playingNotes;
        private readonly ChordFormer _chordFormer;
        public TrackRecorder Recorder { get; }
        public PlayBack PlayBack { get; }

        public PianoController()
        {
            Recorder = new TrackRecorder();
            PlayBack = new PlayBack();
            PlayBack.SendControl += SendControl;
            _playingNotes = new Dictionary<int, HashSet<KeyValuePair<int, int>>>();
            _chordFormer = new ChordFormer();
            _chordFormer.ChordFormed += ChordReceived;
        }

        public void Play()
        {
            PlayBack.Play(Recorder.Recording);
        }

        public void Stop()
        {
            PlayBack.Stop();
        }

        public void SetVoice(VoiceComposition newVoice)
        {
            PlayBack.CurrentVoice = newVoice;
        }

        public static IEnumerable<MidiInCapabilities> GetConnectedInputDevices()
        {
            for(var i = 0; i < MidiIn.NumberOfDevices; ++i)
            {
                yield return MidiIn.DeviceInfo(i);
            }
        }

        public static IEnumerable<MidiOutCapabilities> GetConnectedOutDevices()
        {
            for(var i = 0; i < MidiOut.NumberOfDevices; ++i)
            {
                yield return MidiOut.DeviceInfo(i);
            }
        }

        public void Init(int inDeviceNo, int outDeviceNo)
        {
            if (_initialized)
            {
                _inDevice.Stop();
                _inDevice.Close();
                _inDevice.Dispose();
                _outDevice.Close();
                _outDevice.Dispose();
            }
            _inDevice = new MidiIn(inDeviceNo);
            _outDevice = new MidiOut(outDeviceNo);
            _inDevice.MessageReceived += MessageReceived;
            _inDevice.ErrorReceived += ErrorReceived;
            _inDevice.Start();
            _initialized = true;
        }

        private void RemovePlayingNote(int note)
        {
            if (!_playingNotes.ContainsKey(note)) return;
            foreach (var (channel, noteNr) in _playingNotes[note])
            {
                var midi = new NoteOnEvent(0,channel,noteNr,0,1);
                _outDevice.Send(midi.GetAsShortMessage());
            }

            _playingNotes.Remove(note);
        }

        private void MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            if (e.MidiEvent.CommandCode == MidiCommandCode.TimingClock || e.MidiEvent.CommandCode == MidiCommandCode.AutoSensing) return;
            if (e.MidiEvent is NoteEvent note && note.Channel == Configuration.PlayChannel)
            {
                if (PlayBack.CurrentVoice.ChannelAllocation == null)
                {
                    _outDevice.Send(note.GetAsShortMessage());
                }
                else
                {
                    if (note.Velocity == 0)
                    {
                        RemovePlayingNote(note.NoteNumber);
                    }
                    else
                    {
                        if (!_playingNotes.ContainsKey(note.NoteNumber))
                            _playingNotes[note.NoteNumber] = new HashSet<KeyValuePair<int, int>>();
                        for (var i = 0; i < 16; ++i)
                        {
                            var newNote = new NoteOnEvent(0, i + 1, note.NoteNumber, note.Velocity, 1);
                            if (i >= PlayBack.CurrentVoice.ChannelAllocation.Length) continue;
                            var channelSide = PlayBack.CurrentVoice.ChannelAllocation[i];
                            var splitPoint = PlayBack.CurrentVoice.SplitPoint;
                            newNote.NoteNumber = PlayBack.CurrentVoice.ChannelTransposition[i] + note.NoteNumber;
                            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                            switch (channelSide)
                            {
                                case VoiceComposition.ChannelSide.Left when note.NoteNumber > splitPoint:
                                case VoiceComposition.ChannelSide.Right when note.NoteNumber <= splitPoint:
                                case VoiceComposition.ChannelSide.None:
                                    continue;
                                default:
                                    _outDevice.Send(newNote.GetAsShortMessage());
                                    _playingNotes[note.NoteNumber]
                                        .Add(new KeyValuePair<int, int>(i + 1, newNote.NoteNumber));
                                    break;
                            }
                        }
                    }
                }

                if (note.Velocity != 0) PlayBack.NoteReceived(note.NoteNumber);
            } 
            else if (!(e.MidiEvent is NoteEvent) && !(e.MidiEvent is PatchChangeEvent))
            {
                _outDevice.Send(e.MidiEvent.GetAsShortMessage());
            }

            _chordFormer.NoteReceived(e.MidiEvent);
        }

        private void ChordReceived(Chord chord)
        {
            Recorder.RecordChord(chord);
        }

        private static void ErrorReceived(object sender, MidiInMessageEventArgs e)
        {
            Console.Error.WriteLine("Error: {0}",e.MidiEvent);
        }

        private void SendControl(MidiEvent e)
        {
            _outDevice.Send(e.GetAsShortMessage());
        }

        public void Dispose()
        {
            _inDevice?.Dispose();
            _outDevice?.Dispose();
        }
    }
}
