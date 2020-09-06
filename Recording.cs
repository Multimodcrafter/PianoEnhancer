using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PianoEnhancer
{
    [JsonConverter(typeof(RecordingSerializer))]
    internal class Recording
    {
        public IList<Chord> Track { get; }
        public IDictionary<int,VoiceComposition> VoiceSwitchPoints { get; }

        public Recording(IList<Chord> track)
        {
            Track = track;
            VoiceSwitchPoints = new Dictionary<int, VoiceComposition>();
        }

        public Recording(IList<Chord> track, IDictionary<int, VoiceComposition> switchPoints)
        {
            Track = track;
            VoiceSwitchPoints = switchPoints;
        }

        public static Recording LoadFromFile(string filename)
        {
            string jsonText;
            using (var reader = new StreamReader(filename))
            {
                jsonText = reader.ReadToEnd();
            }
            var result = JsonSerializer.Deserialize<Recording>(jsonText);
            return result;
        }

        public void SaveToFile(string filename)
        {
            using var writer = new StreamWriter(filename);
            writer.Write(JsonSerializer.Serialize(this));
        }

        public IEnumerable<KeyValuePair<int, VoiceComposition>> GetSwitchPoints()
        {
            return VoiceSwitchPoints.OrderBy(x=>x.Key);
        }

        public void SetVoice(int point, VoiceComposition voice)
        {
            if (VoiceSwitchPoints.ContainsKey(point))
            {
                VoiceSwitchPoints[point] = voice;
            }
            else
            {
                VoiceSwitchPoints.Add(point,voice);
            }
        }

        public void RemoveVoice(int point)
        {
            if (VoiceSwitchPoints.ContainsKey(point)) VoiceSwitchPoints.Remove(point);
        }

    }
}
