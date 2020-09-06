using NAudio.Midi;

namespace PianoEnhancer
{
    internal struct BaseVoice
    {
        public string DisplayName { get; set; }
        public byte BankMsb { get; set; }
        public byte BankLsb { get; set; }
        public byte ProgramChangeNumber { get; set; }

        public BaseVoice(string dp, byte msb, byte lsb, byte pc)
        {
            DisplayName = dp;
            BankMsb = msb;
            BankLsb = lsb;
            ProgramChangeNumber = pc;
        }

        public MidiEvent[] GetEventsForChannel(int channel)
        {
            channel += 1;
            return new MidiEvent[]
            {
                new ControlChangeEvent(0,channel,MidiController.BankSelect,BankMsb), 
                new ControlChangeEvent(0,channel,MidiController.BankSelectLsb,BankLsb),
                new PatchChangeEvent(0,channel,ProgramChangeNumber), 
            }
            ;
        }
    }
}
