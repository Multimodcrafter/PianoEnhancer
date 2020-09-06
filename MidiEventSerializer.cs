using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using NAudio.Midi;

namespace PianoEnhancer
{
    internal class MidiEventSerializer : JsonConverter<MidiEvent>
    {
        public override MidiEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return MidiEvent.FromRawMessage(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, MidiEvent value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.GetAsShortMessage());
        }
    }
}
