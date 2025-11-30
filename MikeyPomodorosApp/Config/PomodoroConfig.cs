using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MikeyPomodorosApp.Config
{
    class PomodoroConfig
    {
        [JsonPropertyName("studyTime")]
        public int StudyTime { get; set; } = 25;
        [JsonPropertyName("breakTime")]
        public int BreakTime { get; set; } = 5;
        [JsonPropertyName("longBreaktTime")]
        public int LongBreakTime { get; set; } = 30;
        [JsonPropertyName("cyclesBeforeLongBreak")]
        public int CyclesBeforeLongBreak { get; set; } = 3;
        [JsonPropertyName("autoStartTimer")]
        public bool AutoStartTimer { get; set; } = false;

    }
}
