using System.Collections.Generic;
using System.Linq;

namespace PianoEnhancer
{
    internal static class Voices
    {
        public static readonly BaseVoice[] BaseVoices = {
            new BaseVoice("Grand Piano 1",0,122,0),
            new BaseVoice("Grand Piano 2",0,112,0),
            new BaseVoice("Grand Piano 3",0,123,0),
            new BaseVoice("Piano & Strings",0,125,0),
            new BaseVoice("Electric Piano 1",0,122,5),
            new BaseVoice("Electric Piano 2",0,122,4),
            new BaseVoice("Electric Piano 3",0,123,4),
            new BaseVoice("Church Organ",0,123,19),
            new BaseVoice("Jazz Organ",0,122,16),
            new BaseVoice("Strings",0,122,48),
            new BaseVoice("Harpsichord",0,122,6),
            new BaseVoice("E-Clavichord",0,122,7),
            new BaseVoice("Vibraphone",0,122,11),
            new BaseVoice("Wood Bass",0,122,32),
            new BaseVoice("Bass & Cymbal",0,124,32),
            new BaseVoice("E-Bass 1",0,122,33),
            new BaseVoice("E-Bass 2",0,122,35),
        };

        public static readonly VoiceComposition.ChannelSide[] ChannelSides =
        {
            VoiceComposition.ChannelSide.None,
            VoiceComposition.ChannelSide.Both,
            VoiceComposition.ChannelSide.Right,
            VoiceComposition.ChannelSide.Left
        };

        //Reminder: Add 21 to the index since Notes 0-20 are not available on a keyboard
        public static readonly string[] NoteNames = NoteNamesGenerator().ToArray();

        private static IEnumerable<string> NoteNamesGenerator()
        {
            var octave = 0;
            var baseNotes = new []{"C","C#","D","D#","E","F","F#","G","G#","A","B","H"};
            var interOctave = 9;
            while (octave != 8 || interOctave != 1)
            {
                yield return baseNotes[interOctave] + octave;
                if (++interOctave < baseNotes.Length) continue;
                interOctave = 0;
                ++octave;
            }
        }
    }
}
