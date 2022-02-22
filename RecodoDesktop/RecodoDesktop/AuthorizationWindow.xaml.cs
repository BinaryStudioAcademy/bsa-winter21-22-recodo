using Recodo.Desktop.Logic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace Recodo.Desktop.Main
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {       
            await GetToken(ConfigurationManager.AppSettings["loginUrl"]);
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            await GetToken(ConfigurationManager.AppSettings["registerUrl"]);
        }

        private async Task GetToken(string endpoint)
        {
            this.ProgressPanel.Visibility = Visibility.Visible;
            var auth = new AuthorizationService(endpoint);
            try
            {
                var authResult = await auth.Authorize();
                this.ProgressPanel.Visibility = Visibility.Hidden;
            }
            catch
            {
                this.BrowserState.Text = "Some went wrong, please try";
            }
            this.Activate();
        }
    }
}
