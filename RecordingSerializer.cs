using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PianoEnhancer
{
    internal class RecordingSerializer : JsonConverter<Recording>
    {
        public override Recording Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.Read();
            if(reader.TokenType != JsonTokenType.PropertyName || !reader.ValueTextEquals("Track"))
                throw new InvalidDataException("Expected Track");
            var track = JsonSerializer.Deserialize<List<Chord>>(ref reader,options);
            reader.Read();
            if(reader.TokenType != JsonTokenType.PropertyName || !reader.ValueTextEquals("SwitchPoints"))
                throw new InvalidDataException("Expected SwitchPoints");
            reader.Read();
            if(reader.TokenType != JsonTokenType.StartArray) throw new InvalidDataException("Expected start of SwitchPoints list");
            reader.Read();
            var switchPoints = new Dictionary<int,VoiceComposition>();
            var customOptions = new JsonSerializerOptions();
            customOptions.Converters.Add(new MidiEventSerializer());
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                var point = reader.GetInt32();
                reader.Read();
                var voice = JsonSerializer.Deserialize<VoiceComposition>(ref reader,customOptions);
                switchPoints[point] = voice;
                reader.Read();
            }

            reader.Read();
            return new Recording(track,switchPoints);
        }

        public override void Write(Utf8JsonWriter writer, Recording value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Track");
            JsonSerializer.Serialize(writer, value.Track, options);

            var customOptions = new JsonSerializerOptions();
            customOptions.Converters.Add(new MidiEventSerializer());

            writer.WriteStartArray("SwitchPoints");
            foreach (var (point, voice) in value.VoiceSwitchPoints)
            {
                writer.WriteNumberValue(point);
                JsonSerializer.Serialize(writer,voice,customOptions);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
