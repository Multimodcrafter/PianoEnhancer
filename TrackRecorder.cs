using System;
using System.Collections.Generic;

namespace PianoEnhancer
{
    internal class TrackRecorder
    {
        private long _recordStartTime;
        private long _recordLength;
        public bool IsRecording { get; private set; }

        public Recording Recording
        {
            get { return _recording ??= new Recording(_track); }
        }

        private Recording _recording;
        private IList<Chord> _track;

        public TrackRecorder()
        {
            _track = new List<Chord>();
        }

        public void CreateNewRecording()
        {
            _track = new List<Chord>();
            _recording = null;
            IsRecording = false;
            _recordLength = 0;
        }

        public void StartRecording()
        {
            IsRecording = true;
            _recordStartTime = DateTime.Now.Ticks/ Configuration.TimeResolution;
        }

        public void StopRecording()
        {
            IsRecording = false;
            _recordLength += DateTime.Now.Ticks / Configuration.TimeResolution - _recordStartTime;
        }

        public void SaveRecording(string filename)
        {
            Recording.SaveToFile(filename);
        }

        public void LoadRecording(string filename)
        {
            _recording = Recording.LoadFromFile(filename);
        }

        public void RecordChord(Chord chord)
        {
            if (!IsRecording) return;
            var correctedChord = new Chord(_recordLength + DateTime.Now.Ticks/ Configuration.TimeResolution - _recordStartTime,chord.Notes);
            _track.Add(correctedChord);
        }
    }
}
