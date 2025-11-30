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
        private System.Timers.Timer fadeOutTimer;

        public MusicPlayer()
        {
            player = new MediaPlayer();
            studySongs = Directory.GetFiles(@".\Study Music").ToList();
            breakSongs = Directory.GetFiles(@".\Break Music").ToList();
            longBreakSongs = Directory.GetFiles(@".\Long Break Music").ToList();
            player.MediaEnded += Player_MediaEnded;
        }

        private void Player_MediaEnded(object? sender, EventArgs e)
        {
            player.Open(new Uri(currentPlaylist[0], UriKind.Relative));
            currentPlaylist.RemoveAt(0);
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
            player.Volume = 0.5;
            player.Play();
        }

        public void Stop()
        {
            player.Stop();
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


        public void addSongsToPlaylist(List<string> filenames)
        {
            currentPlaylist = filenames.OrderBy(i => Guid.NewGuid()).ToList();
            player.Open(new Uri(currentPlaylist[0], UriKind.Relative));
            currentPlaylist.RemoveAt(0);
        }

    }
}
