using MikeyPomodorosApp.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;

namespace MikeyPomodorosApp
{
    public class MusicPlayer
    {
        public MediaPlayer player;
        public List<string> studySongs;
        public List<string> breakSongs;
        public List<string> longBreakSongs;
        public List<string> currentPlaylist;
        public int playlistIndex;
        public int studyIndex = 1;
        public int breakIndex = 1;
        public int lbIndex = 1;
        public bool loop;
        private System.Timers.Timer fadeOutTimer;

        public MusicPlayer()
        {
            player = new MediaPlayer();
            if (ConfigManager._config.UsePlaylist())
            {
                studySongs = ConfigManager._config.PomodoroPlaylist.StudySongs;
                breakSongs = ConfigManager._config.PomodoroPlaylist.BreakSongs;
                longBreakSongs = ConfigManager._config.PomodoroPlaylist.LongBreakSongs;
            }
            else
            {
                studySongs = Directory.GetFiles(@".\Study Music").ToList();
                breakSongs = Directory.GetFiles(@".\Break Music").ToList();
                longBreakSongs = Directory.GetFiles(@".\Long Break Music").ToList();
            }
            player.MediaEnded += Player_MediaEnded;
            player.Volume = MainWindow.volumeSlider.Value / 100;
            loop = true;
            playlistIndex = 1;
        }

        private void Player_MediaEnded(object? sender, EventArgs e)
        {
            if (loop)
            {
                player.Position = TimeSpan.Zero;
                player.Play();
            }
            else
            {
                playNextSong();
                player.Play();
            }
        }

        public void playNextSong()
        {
            player.Open(new Uri(currentPlaylist[playlistIndex], UriKind.Relative));
            if (currentPlaylist.Count <= playlistIndex + 1 )
            {
                currentPlaylist = currentPlaylist.OrderBy(i => Guid.NewGuid()).ToList();
                playlistIndex = 0;  
            }
            else
            {
                playlistIndex++;
            }
        }

        public void loadPlaylist(TimerType type)
        {
            switch (type)
            {
                case TimerType.Study:
                    addSongsToPlaylist(studySongs);
                break;
                case TimerType.Break:
                    addSongsToPlaylist(breakSongs);
                break;
                case TimerType.LongBreak:
                    addSongsToPlaylist(longBreakSongs);
                break;
            }
        }

        public void Play()
        {
            if (player.HasAudio == true)
            {
                player.Play();
            }
            else
            {
                playNextSong();
                player.Play();
            }
        }

        public void Stop()
        {
            player.Dispatcher.Invoke(player.Pause);
        }

        public void FadeOut()
        {
            fadeOutTimer = new System.Timers.Timer();
            fadeOutTimer.Interval = 100;
            fadeOutTimer.Elapsed += new ElapsedEventHandler(FadeOutTimer_Tick);
            fadeOutTimer.Start();
        }

        public void FadeOutTimer_Tick(object source , ElapsedEventArgs e)
        {
            player.Volume -= 0.01;
            if (player.Volume <= 0)
            {
                fadeOutTimer.Stop();
            }
        }

        public void ChangeVolume(double volume)
        {
            player.Volume = volume/100;
        }

        private void addSongsToPlaylist(List<string> filenames)
        {
            currentPlaylist = ConfigManager._config.Shuffle ? filenames.OrderBy(i => Guid.NewGuid()).ToList() : filenames.ToList();
            player.Dispatcher.Invoke(() => player.Open(new Uri(currentPlaylist[playlistIndex - 1 ], UriKind.Relative)));
        }

        public void saveLoadPlaylistIndex(TimerType typefrom, TimerType typeTo)
        {
            switch (typefrom)
            {
                case TimerType.Study:
                    studyIndex = playlistIndex + 1 ;
                    break;
                case TimerType.Break:
                    breakIndex = playlistIndex + 1;
                    break;
                case TimerType.LongBreak:
                    lbIndex = playlistIndex + 1;
                    break;
            }
            switch (typeTo)
            {
                case TimerType.Study:
                    playlistIndex = studyIndex;
                    break;
                case TimerType.Break:
                    playlistIndex = breakIndex;
                    break;
                case TimerType.LongBreak:
                    playlistIndex = lbIndex;
                    break;
            }
        }


    }
}
