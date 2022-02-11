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

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            this.ProgressPanel.Visibility = Visibility.Visible;
            BrowserService.OpenUrl("https://recodo.westeurope.cloudapp.azure.com/");
            //Todo: get authUser token and user info
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            this.ProgressPanel.Visibility = Visibility.Visible;
            BrowserService.OpenUrl("https://recodo.westeurope.cloudapp.azure.com/");
        }
    }
}
