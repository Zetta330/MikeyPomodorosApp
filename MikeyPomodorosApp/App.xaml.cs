using MikeyPomodorosApp.Config;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MikeyPomodorosApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    void App_Startup(object sender, StartupEventArgs e)
    {
        //load the config
        ConfigManager.ReadConfiguration();
    }
}

