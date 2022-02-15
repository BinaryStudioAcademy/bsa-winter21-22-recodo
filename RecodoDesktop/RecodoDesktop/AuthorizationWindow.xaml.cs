using Recodo.Desktop.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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
            this.ProgressPanel.Visibility = Visibility.Visible;
            var auth = new AuthorizationService(new AuthorizationOptions());
            var authResult = await auth.Authorize();
            this.ProgressPanel.Visibility = Visibility.Hidden;
            this.Activate();
            MessageBox.Show($"Code: {authResult}\n");
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            this.ProgressPanel.Visibility = Visibility.Visible;
            DefaultBrowser.Open("https://recodo.westeurope.cloudapp.azure.com/");
        }
    }
}
