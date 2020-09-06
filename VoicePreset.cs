using System.Collections.Generic;
using NAudio.Midi;

namespace PianoEnhancer
{
    internal struct VoicePreset
    {
        
        public string PresetName { get; set; }
        public int SplitPoint { get; set; }
        public BaseVoice[] Voice { get; set; }
        public VoiceComposition.ChannelSide[] Allocation { get; set; }
        public int[] Transposition { get; set; }

        public VoicePreset(string pn, int sp, BaseVoice[] v, VoiceComposition.ChannelSide[] a, int[] t)
        {
            PresetName = pn;
            SplitPoint = sp;
            Voice = v;
            Allocation = a;
            Transposition = t;
        }

        public VoiceComposition ToVoiceComposition()
        {
            var channelVoice = new List<MidiEvent>();

            for (var i = 0; i < Configuration.TotalChannels; ++i)
            {
                if(Allocation[i] != VoiceComposition.ChannelSide.None)
                    channelVoice.AddRange(Voice[i].GetEventsForChannel(i));
            }

            return new VoiceComposition(SplitPoint + 21,channelVoice,Allocation,Transposition);
        }
    }
}
