using System.Linq;

namespace PianoEnhancer
{
    internal struct Chord
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Chord other)
        {
            return AbsoluteTime == other.AbsoluteTime && Equals(Notes, other.Notes);
        }

        public override bool Equals(object obj)
        {
            return obj is Chord other && Equals(other);
        }

        public long AbsoluteTime { get; set; }
        public int[] Notes { get; set; }

        public Chord(long time, int[] notes)
        {
            AbsoluteTime = time;
            Notes = notes;
        }

        public static bool operator==(Chord a, Chord b)
        {
            return a.Notes.Length == b.Notes.Length && b.Notes.All(bNote => a.Notes.Contains(bNote));
        }

        public static bool operator!=(Chord a, Chord b)
        {
            return a.Notes.Length != b.Notes.Length || a.Notes.Any(n => !b.Notes.Contains(n));
        }
    }
}
