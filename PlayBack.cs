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
        private IList<KeyValuePair<int, VoiceComposition>> _voiceSwitchPoints;
        private IList<Chord> _bufferedChords;
        private long _newestMatched;
        public int PendingPosition { get; private set; }
        private IEnumerator<Chord> _pendingPointer;
        private IEnumerator<KeyValuePair<int, VoiceComposition>> _voicePointer;
        private int _playedSinceLastMatch;

        public PlayBack()
        {
            _bufferedChords = new List<Chord>();
        }

        public void Play(Recording rec)
        {
            if (IsPlaying) return;
            _pendingChords = rec.Track;
            _voiceSwitchPoints = rec.GetSwitchPoints().ToList();
            lock (_bufferedChords)
            {
                _bufferedChords = new List<Chord>();
            }

            _newestMatched = 0;
            PendingPosition = 0;
            _playedSinceLastMatch = 0;
            _pendingPointer = _pendingChords.GetEnumerator();
            _voicePointer = _voiceSwitchPoints.GetEnumerator();
            _voicePointer.MoveNext();
            _pendingPointer.MoveNext();
            IsPlaying = true;
            var worker = new Task(WorkerLoop);
            worker.Start();
        }

        public void Stop()
        {
            IsPlaying = false;
            PlaybackStopped?.Invoke();
        }

        public void ChordReceived(Chord chord)
        {
            if (!IsPlaying) return;
            lock (_bufferedChords)
            {
                _bufferedChords.Add(chord);
                _playedSinceLastMatch++;
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

        private void RemoveSurpassedNotes()
        {
            var oldestPending = _pendingPointer.Current;
            if (_newestMatched < oldestPending.AbsoluteTime &&
                _playedSinceLastMatch <= Configuration.ConsumeWindowSteps) return;
            if(!_pendingPointer.MoveNext())
            {
                Stop();
                return;
            }
            PendingPosition++;
            _playedSinceLastMatch--;
        }

        private void MatchNotes()
        {
            lock (_bufferedChords)
            {
                for (var i = 0; i < _bufferedChords.Count; ++i)
                {
                    var chord = _bufferedChords[i];
                    var first = _pendingPointer.Current;
                    if (first.Notes == null)
                    {
                        Stop();
                        return;
                    }
                    for(var j = PendingPosition;  j < _pendingChords.Count; ++j)
                    {
                        var candidate = _pendingChords[j];
                        if (j - PendingPosition > Configuration.ConsumeWindowSteps)
                        {
                            _bufferedChords.RemoveAt(i);
                            --i;
                            break;
                        }
                        if (candidate != chord) continue;
                        _bufferedChords.RemoveAt(i);
                        _newestMatched = candidate.AbsoluteTime;
                        _playedSinceLastMatch = 0;
                        --i;
                        break;
                    }
                }
            }
        }
    }
}
