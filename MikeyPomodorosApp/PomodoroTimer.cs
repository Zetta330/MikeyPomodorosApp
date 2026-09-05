using MikeyPomodorosApp.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MikeyPomodorosApp
{

    public class PomodoroTimer
    {
        public int secondsLeft { get; set; }
        public int cyclesUntilLongBreak { get; set; }
        public TimerType timerType { get; set; }
        public System.Timers.Timer seconds { get; set; }
        private static SoundPlayer studySound = ConfigManager._config.PomodoroPlaylist.StudyAlarm != null ? new SoundPlayer(ConfigManager._config.PomodoroPlaylist.StudyAlarm) : new SoundPlayer(@".\Sounds\studytime.wav");
        private static SoundPlayer breakSound = ConfigManager._config.PomodoroPlaylist.BreakAlarm != null ? new SoundPlayer(ConfigManager._config.PomodoroPlaylist.BreakAlarm) : new SoundPlayer(@".\Sounds\breaktime.wav");
        public MusicPlayer player;


        public PomodoroTimer()
        {
            seconds = new System.Timers.Timer();
            seconds.Interval = 1000;
            seconds.Elapsed += new ElapsedEventHandler(runTimer);
            timerType = TimerType.Study;
            secondsLeft = ConfigManager._config.StudyTime * 60;
            cyclesUntilLongBreak = ConfigManager._config.CyclesBeforeLongBreak;
            player = new MusicPlayer();
            player.loadPlaylist(timerType);
        }

        public void startTimer()
        {
            seconds.Enabled = true;
            player.Play();
        }

        //looping clock that ticks down our timer
        private void runTimer(Object source, ElapsedEventArgs e)
        {
            string timerText;
            if(seconds.Enabled = true && secondsLeft > 0)
            {
                secondsLeft--;
                timerText = $"{secondsLeft / 60}:{(secondsLeft % 60 < 10 ? "0" : "")}{secondsLeft % 60}";
                MainWindow.timerText.Dispatcher.Invoke(() =>
                {
                    MainWindow.timerText.Text = timerText;
                });
                seconds.Interval = 1000;
                seconds.Enabled = true;
            }
            else
            {
                stopTimer();
            }  
        }
        
        //if there is still time, pause the timer.
        //if there is no time left, set up the timer for the next phase.
        public void stopTimer()
        {
            seconds.Enabled = false;
            player.Stop();
            MainWindow.startStopButton.Dispatcher.Invoke(() =>
            {
                MainWindow.startStopButton.Content = "Start";
            });

            if (secondsLeft == 0)
            {
                if (timerType == TimerType.Study){
                    breakSound.Play();
                    if (cyclesUntilLongBreak == 0)
                    {
                        player.saveLoadPlaylistIndex(timerType, TimerType.LongBreak);
                        timerType = TimerType.LongBreak;
                        secondsLeft = ConfigManager._config.LongBreakTime * 60;
                        MainWindow.statusText.Dispatcher.Invoke(() => MainWindow.statusText.Text = "Long Break!");
                        var timerText = $"{secondsLeft / 60}:{(secondsLeft % 60 < 10 ? "0" : "")}{secondsLeft % 60}";
                        MainWindow.timerText.Dispatcher.Invoke(() => MainWindow.timerText.Text = timerText);
                    }
                    else
                    {
                        player.saveLoadPlaylistIndex(timerType, TimerType.Break);
                        timerType = TimerType.Break;
                        secondsLeft = ConfigManager._config.BreakTime * 60;
                        MainWindow.statusText.Dispatcher.Invoke(() => MainWindow.statusText.Text = "Break Time!");
                        var timerText = $"{secondsLeft / 60}:{(secondsLeft % 60 < 10 ? "0" : "")}{secondsLeft % 60}";
                        MainWindow.timerText.Dispatcher.Invoke(() => MainWindow.timerText.Text = timerText);
                        cyclesUntilLongBreak -= 1;
                    }
                }
                else
                {
                    studySound.Play();
                    player.saveLoadPlaylistIndex(timerType, TimerType.Study);
                    timerType = TimerType.Study;
                    secondsLeft = ConfigManager._config.StudyTime * 60;
                    MainWindow.statusText.Dispatcher.Invoke(() => MainWindow.statusText.Text = "Study Time!");
                    var timerText = $"{secondsLeft / 60}:{(secondsLeft % 60 < 10 ? "0" : "")}{secondsLeft % 60}";
                    MainWindow.timerText.Dispatcher.Invoke(() => MainWindow.timerText.Text = timerText);
                }
                player.loadPlaylist(timerType);
                if (ConfigManager._config.AutoStartTimer)
                {
                    startTimer();
                    MainWindow.startStopButton.Dispatcher.Invoke(() =>
                    {
                        MainWindow.startStopButton.Content = "Pause";
                    });
                }
            }
        }

    }

}
