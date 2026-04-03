using MikeyPomodorosApp.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Settings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            StudyMinsBox.Text = ConfigManager._config.StudyTime.ToString();
            BreakMinsBox.Text = ConfigManager._config.BreakTime.ToString();
            LbMinsBox.Text = ConfigManager._config.LongBreakTime.ToString();
            CyclesBeforeLongBreakBox.Text = ConfigManager._config.CyclesBeforeLongBreak.ToString();
            AutoStartBox.IsChecked = ConfigManager._config.AutoStartTimer;
            ShuffleBox.IsChecked = ConfigManager._config.Shuffle;
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
        }
    }
}
