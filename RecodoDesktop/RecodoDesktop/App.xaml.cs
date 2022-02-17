using Microsoft.Extensions.DependencyInjection;
using Recodo.Desktop.Logic;
using Recodo.Desktop.Main;
using System.Windows;

namespace RecodoDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<RecorderService>();
            services.AddSingleton<VideoRecordingForm>();
            services.AddSingleton<StickPanel>();
            //services.AddSingleton<Countdown>();
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            VideoRecordingForm videoRecordingForm = serviceProvider.GetService<VideoRecordingForm>();
            videoRecordingForm.Show();

            StickPanel stickPanel = serviceProvider.GetService<StickPanel>();
            stickPanel.Show();
        }
    }
}
