using System.Collections.Generic;
using NAudio.Midi;

namespace PianoEnhancer
{
    internal struct VoiceComposition
    {
        public enum ChannelSide
        {
            Left = 3, Right = 2, Both = 1, None = 0
        }

        public int SplitPoint { get; set; }
        public IList<MidiEvent> ChannelVoice { get; set; }
        public ChannelSide[] ChannelAllocation { get; set; }
        public int[] ChannelTransposition { get; set; }

        public VoiceComposition(int sp, IList<MidiEvent> cv, ChannelSide[] ca, int[] ct)
        {
            SplitPoint = sp;
            ChannelVoice = cv;
            ChannelAllocation = ca;
            ChannelTransposition = ct;
        }
    }
}
