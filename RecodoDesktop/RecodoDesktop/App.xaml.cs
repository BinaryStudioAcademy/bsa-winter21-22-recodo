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
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
        }
    }
}
