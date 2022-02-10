using Recodo.Desktop.Main;
using System.Windows;

namespace RecodoDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StickPanel stickPanel = new();
            stickPanel.Show();
        }
    }
}
