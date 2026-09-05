using Microsoft.Win32;
using MikeyPomodorosApp.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MikeyPomodorosApp
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private MainWindow mainWindow;
        public Settings(MainWindow window)
        {
            InitializeComponent();
            LoadSettings();
            mainWindow = window;
        }

        private void LoadSettings()
        {
            StudyMinsBox.Text = ConfigManager._config.StudyTime.ToString();
            BreakMinsBox.Text = ConfigManager._config.BreakTime.ToString();
            LbMinsBox.Text = ConfigManager._config.LongBreakTime.ToString();
            CyclesBeforeLongBreakBox.Text = ConfigManager._config.CyclesBeforeLongBreak.ToString();
            AutoStartBox.IsChecked = ConfigManager._config.AutoStartTimer;
            ShuffleBox.IsChecked = ConfigManager._config.Shuffle;
            if (ConfigManager._config.PomodoroPlaylist != null)
            {
                foreach (var i in ConfigManager._config.PomodoroPlaylist.StudySongs)
                    StudySongsList.Items.Add(i);
                foreach (var j in ConfigManager._config.PomodoroPlaylist.BreakSongs)
                    BreakSongsList.Items.Add(j);
                foreach (var k in ConfigManager._config.PomodoroPlaylist.LongBreakSongs)
                    LbSongList.Items.Add(k);
                StudyAlarmBox.Text = ConfigManager._config.PomodoroPlaylist.StudyAlarm;
                BreakAlarmBox.Text = ConfigManager._config.PomodoroPlaylist.BreakAlarm;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(StudyMinsBox.Text, out var studytime))
                ConfigManager._config.StudyTime = studytime;
            if (int.TryParse(BreakMinsBox.Text, out var breaktime))
                ConfigManager._config.BreakTime = breaktime;
            if (int.TryParse(LbMinsBox.Text, out var lbtime))
                ConfigManager._config.LongBreakTime = lbtime;
            if (int.TryParse(CyclesBeforeLongBreakBox.Text, out var cblb))
                ConfigManager._config.CyclesBeforeLongBreak = cblb;
            ConfigManager._config.AutoStartTimer = AutoStartBox.IsChecked.Value;
            ConfigManager._config.Shuffle = ShuffleBox.IsChecked.Value;
            ConfigManager.SaveConfiguration();
            mainWindow.ReloadConfig();
            this.Close();
        }

        private void AddStudySongsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio files (*.mp3;*.wav;*.flac)|*.mp3;*.wav;*.flac|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog().Value)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                foreach (string file in selectedFiles)
                {
                    ConfigManager._config.PomodoroPlaylist.StudySongs.Add(file);
                    StudySongsList.Items.Add(file);
                }
            }
        }

        private void RemoveStudySongsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = StudySongsList.SelectedItems.Cast<object>().ToList();

            foreach (var item in selectedItems)
            {
                ConfigManager._config.PomodoroPlaylist.StudySongs.Remove(item.ToString());
                StudySongsList.Items.Remove(item);
            }
        }

        private void AddBreakSongsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio files (*.mp3;*.wav;*.flac)|*.mp3;*.wav;*.flac|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog().Value)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                foreach (string file in selectedFiles)
                {
                    ConfigManager._config.PomodoroPlaylist.BreakSongs.Add(file);
                    BreakSongsList.Items.Add(file);
                }
            }
        }

        private void RemoveBreakSongsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = BreakSongsList.SelectedItems.Cast<object>().ToList();

            foreach (var item in selectedItems)
            {
                ConfigManager._config.PomodoroPlaylist.BreakSongs.Remove(item.ToString());
                BreakSongsList.Items.Remove(item);
            }
        }

        private void AddLbSongsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio files (*.mp3;*.wav;*.flac)|*.mp3;*.wav;*.flac|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog().Value)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                foreach (string file in selectedFiles)
                {
                    ConfigManager._config.PomodoroPlaylist.LongBreakSongs.Add(file);
                    LbSongList.Items.Add(file);
                }
            }
        }

        private void RemoveLbSongsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = LbSongList.SelectedItems.Cast<object>().ToList();

            foreach (var item in selectedItems)
            {
                ConfigManager._config.PomodoroPlaylist.LongBreakSongs.Remove(item.ToString());
                LbSongList.Items.Remove(item);
            }
        }

        private void SavePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Playlist";
            saveFileDialog.DefaultExt = ".json";
            saveFileDialog.Filter = "JSON (.json)|*.json|All files (*.*)|*.*";

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = saveFileDialog.FileName;
                System.IO.File.WriteAllText(filePath, JsonSerializer.Serialize(ConfigManager._config.PomodoroPlaylist));
                ConfigManager._config.PlaylistLocation = filePath;
            }
        }

        private void ClearPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigManager._config.PlaylistLocation = "";
            ClearPlaylist();
        }

        private void LoadPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON (.json)|*.json|All files (*.*)|*.*";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                ClearPlaylist();
                string filePath = openFileDialog.FileName;
                ConfigManager._config.PlaylistLocation = filePath;
                var playlistjson = File.ReadAllText(filePath);
                ConfigManager._config.PomodoroPlaylist = JsonSerializer.Deserialize<PomodoroPlaylist>(playlistjson);
                foreach (var i in ConfigManager._config.PomodoroPlaylist.StudySongs)
                    StudySongsList.Items.Add(i);
                foreach (var j in ConfigManager._config.PomodoroPlaylist.BreakSongs)
                    BreakSongsList.Items.Add(j);
                foreach (var k in ConfigManager._config.PomodoroPlaylist.LongBreakSongs)
                    LbSongList.Items.Add(k);
                StudyAlarmBox.Text = ConfigManager._config.PomodoroPlaylist.StudyAlarm;
                BreakAlarmBox.Text = ConfigManager._config.PomodoroPlaylist.BreakAlarm;
            }
        }

        private void ClearPlaylist()
        {
            ConfigManager._config.PomodoroPlaylist = new PomodoroPlaylist();
            StudySongsList.Items.Clear();
            BreakSongsList.Items.Clear();
            LbSongList.Items.Clear();
            BreakAlarmBox.Clear();
            StudyAlarmBox.Clear();
        }

        private void ChangeStudyAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog().Value)
            {
                ConfigManager._config.PomodoroPlaylist.StudyAlarm = openFileDialog.FileName;
                StudyAlarmBox.Text = openFileDialog.FileName;
            }
        }

        private void ChangeBreakAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog().Value)
            {
                ConfigManager._config.PomodoroPlaylist.BreakAlarm = openFileDialog.FileName;
                BreakAlarmBox.Text = openFileDialog.FileName;
            }
        }
    }
}
