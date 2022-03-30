using Microsoft.Win32;
using Recodo.Desktop.Logic;
using Recodo.Desktop.Models.Auth;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            if(!CheckSavedToken())
                InitializeComponent();
            else
            {
                this.Hide();
                OpenRecordingForm();
            }
        }

        private Token token;

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {       
            await GetToken(ConfigurationManager.AppSettings["recodoUrl"] + "login");
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            await GetToken(ConfigurationManager.AppSettings["recodoUrl"] + "register");
        }

        private async Task GetToken(string endpoint)
        {
            this.ProgressPanel.Visibility = Visibility.Visible;
            var auth = new AuthorizationService(endpoint);
            try
            {
                token = await auth.Authorize();
                RegistryHelper.SaveToken(token.AccessToken);
                this.ProgressPanel.Visibility = Visibility.Hidden;
            }
            catch
            {
                this.DeterminateCircularProgress.Visibility = Visibility.Hidden;
                this.BrowserState.Text = "Some went wrong, please try again..";
            }

            this.Hide();
            OpenRecordingForm();
        }

        private bool CheckSavedToken()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Recodo");
            if(key?.GetValue("token") is not null)
            {
                token = new Token(key.GetValue("token").ToString(), "");
                return true;
            }
            else
            {
                return false;
            }
        }
        private void OpenRecordingForm()
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token.AccessToken.Trim('"'));
            var workspaceName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value;

            RecorderService recorderService = new RecorderService(token);
            VideoRecordingForm recordingForm = new VideoRecordingForm(recorderService, workspaceName);
            recordingForm.Show();
        }
    }
}
