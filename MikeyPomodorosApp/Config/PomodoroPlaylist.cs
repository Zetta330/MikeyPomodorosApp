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
        public List<string> StudySongs { get; set; }

        [JsonPropertyName("breakSongs")]
        public List<string> BreakSongs { get; set; }

        [JsonPropertyName("longBreakSongs")]
        public List<string> LongBreakSongs { get; set; }

    }
}
