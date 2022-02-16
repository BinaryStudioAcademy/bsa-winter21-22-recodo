using Recodo.Desktop.Main;
using System.Windows;

namespace RecodoDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            VideoRecordingForm videoRecordingForm = new();
            videoRecordingForm.Show();

            StickPanel stickPanel = new();
            stickPanel.Show();
        }
    }
}
