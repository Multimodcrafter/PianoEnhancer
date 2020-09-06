using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Midi;

namespace PianoEnhancer
{
    internal class ChordFormer
    {
        public delegate void ChordFormResult(Chord chord);

        public event ChordFormResult ChordFormed;

        private readonly HashSet<int> _currentNotes;

        private Task _chordTimeoutTask;

        public ChordFormer()
        {
            _currentNotes = new HashSet<int>();
            _chordTimeoutTask = new Task(TimeOutChord);
        }

        public void NoteReceived(MidiEvent e)
        {
            if (e.Channel != Configuration.PlayChannel || !(e is NoteEvent)) return;
            var note = (NoteEvent) e;
            if(note.Velocity == 0) FormChord();
            else
            {
                _currentNotes.Add(note.NoteNumber);
                if (_chordTimeoutTask.Status == TaskStatus.Created) _chordTimeoutTask.Start();
                else if (_chordTimeoutTask.Status != TaskStatus.Running)
                {
                    _chordTimeoutTask = new Task(TimeOutChord);
                    _chordTimeoutTask.Start();
                } 
            }
        }

        private void FormChord()
        {
            if (_currentNotes.Count == 0) return;
            var chord = new Chord(0,_currentNotes.ToArray());
            _currentNotes.Clear();
            ChordFormed?.Invoke(chord);
        }

        private void TimeOutChord()
        {
            Thread.Sleep(Configuration.ChordTimeoutMs);
            FormChord();
        }
    }
}
