using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MikeyPomodorosApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static Button startStopButton;
    public static TextBlock timerText;
    public static TextBlock statusText;
    public PomodoroTimer pomodoroTimer;
    public MainWindow()
    {
        InitializeComponent();
        startStopButton = start_stop_button;
        timerText = timer_text_block;
        statusText = status_text_block;
        pomodoroTimer = new PomodoroTimer();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
       if(pomodoroTimer.seconds.Enabled == false)
        {
            pomodoroTimer.startTimer();
            startStopButton.Content = "Pause";
        }
        else
        {
            pomodoroTimer.stopTimer();
            startStopButton.Content = "Resume";
        }
    }

}