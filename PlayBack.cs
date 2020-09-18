using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NAudio.Midi;

namespace PianoEnhancer
{
    internal class PlayBack
    {
        public delegate void MidiEventReceiver(MidiEvent ev);

        public delegate void StopEvent();
        public event MidiEventReceiver SendControl;
        public event StopEvent PlaybackStopped;

        public VoiceComposition CurrentVoice
        {
            set 
            {
                foreach (var voice in value.ChannelVoice)
                {
                    SendControl?.Invoke(voice);
                }

                _currentVoice = value;
            }
            get => _currentVoice;
        }
        
        private VoiceComposition _currentVoice;
        public bool IsPlaying { get; private set; }
        private IList<Chord> _pendingChords;
        private IList<NoteOnEvent> _pendingNotes;
        private IList<KeyValuePair<int, VoiceComposition>> _voiceSwitchPoints;
        private IList<int> _bufferedNotes;
        private long _newestMatched;
        public int PendingPosition { get; private set; }
        private int _matchedNotes;
        private IEnumerator<KeyValuePair<int, VoiceComposition>> _voicePointer;

        public PlayBack()
        {
            _bufferedNotes = new List<int>();
        }

        public void Play(Recording rec)
        {
            if (IsPlaying) return;
            _pendingChords = rec.Track;
            _pendingNotes = _pendingChords.SelectMany(
                chord => chord.Notes.Select(note => new NoteOnEvent(chord.AbsoluteTime,1,note,1,1))
                ).ToList();
            _voiceSwitchPoints = rec.GetSwitchPoints().ToList();
            lock (_bufferedNotes)
            {
                _bufferedNotes = new List<int>();
            }

            _newestMatched = 0;
            PendingPosition = 0;
            _matchedNotes = 0;
            _voicePointer = _voiceSwitchPoints.GetEnumerator();
            _voicePointer.MoveNext();
            IsPlaying = true;
            var worker = new Task(WorkerLoop);
            worker.Start();
        }

        public void Stop()
        {
            IsPlaying = false;
            PlaybackStopped?.Invoke();
        }

        public void NoteReceived(int note)
        {
            if (!IsPlaying) return;
            lock (_bufferedNotes)
            {
                _bufferedNotes.Add(note);
            }
        }

        private void WorkerLoop()
        {
            while (IsPlaying)
            {
                ConsumeWork();
            }
        }

        private void ConsumeWork()
        {
            RemoveSurpassedNotes();
            MatchNotes();
            SetVoice();
        }

        private void SetVoice()
        {
            if (PendingPosition < _voicePointer.Current.Key) return;
            _currentVoice = _voicePointer.Current.Value;
            if (!_voicePointer.MoveNext())
            {
                Stop();
            }

            if (_currentVoice.ChannelVoice == null) return;
            foreach (var voice in _currentVoice.ChannelVoice)
            {
                SendControl?.Invoke(voice);
            }
        }

        private void IncrementMatchedNotes()
        {
            _matchedNotes++;
            if (_matchedNotes != _pendingChords[PendingPosition].Notes.Length) return;
            _matchedNotes = 0;
            PendingPosition++;
        }

        private void RemoveSurpassedNotes()
        {
            var oldestPending = _pendingNotes[0];
            if (_newestMatched - Configuration.PendingNoteDiscardThreshold <= oldestPending.AbsoluteTime) return;
            if (_pendingNotes.Count <= 1)
            {
                Stop();
                return;
            }
            _pendingNotes.RemoveAt(0);
            IncrementMatchedNotes();
        }

        private void MatchNotes()
        {
            if (_pendingNotes.Count == 0)
            {
                Stop();
                return;
            }
            lock (_bufferedNotes)
            {
                for (var i = 0; i < _bufferedNotes.Count; ++i)
                {
                    var note = _bufferedNotes[i];
                    for(var j = 0;  j < _pendingNotes.Count; ++j)
                    {
                        var candidate = _pendingNotes[j];
                        if (j > Configuration.ConsumeWindowSteps)
                        {
                            _bufferedNotes.RemoveAt(i);
                            --i;
                            break;
                        }
                        if (candidate.NoteNumber != note) continue;
                        _bufferedNotes.RemoveAt(i);
                        _pendingNotes.RemoveAt(j);
                        _newestMatched = candidate.AbsoluteTime;
                        --i;
                        IncrementMatchedNotes();
                        break;
                    }
                }
            }
        }
    }
}
