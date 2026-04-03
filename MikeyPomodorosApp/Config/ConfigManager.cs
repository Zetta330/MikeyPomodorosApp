using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MikeyPomodorosApp.Config
{
    class ConfigManager
    {
        public static PomodoroConfig _config = new PomodoroConfig();

        public static void SaveConfiguration()
        {
            string json = JsonSerializer.Serialize(_config);
            File.WriteAllText("pomodoro.config", json);
        }

        public static void ReadConfiguration()
        {
            if(File.Exists("pomodoro.config"))
            { 
                string json = File.ReadAllText("pomodoro.config");
                _config = JsonSerializer.Deserialize<PomodoroConfig>(json);
                if (_config.UsePlaylist())
                {
                    var playlistjson = File.ReadAllText(_config.PlaylistLocation);
                    _config.PomodoroPlaylist = JsonSerializer.Deserialize<PomodoroPlaylist>(playlistjson);
                }
            }
            else
            {
                //if we get nothing back, make a new config
                SaveConfiguration();
            }
            
        }
    }
}
