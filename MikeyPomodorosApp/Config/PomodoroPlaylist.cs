using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MikeyPomodorosApp.Config
{
    class PomodoroPlaylist
    {
        [JsonPropertyName("studySongs")]
        public List<string> StudySongs { get; set; } = new List<string>();

        [JsonPropertyName("breakSongs")]
        public List<string> BreakSongs { get; set; } = new List<string>();

        [JsonPropertyName("longBreakSongs")]
        public List<string> LongBreakSongs { get; set; } = new List<string>();

        [JsonPropertyName("studyAlarm")]
        public string StudyAlarm { get; set; }

        [JsonPropertyName("breakAlarm")]
        public string BreakAlarm { get; set; }
    }
}
