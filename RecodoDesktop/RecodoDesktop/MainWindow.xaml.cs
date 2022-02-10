using System.Windows;
using System.Windows.Input;

namespace RecodoDesktop
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Top = 70;
            this.Left = 0;
            this.Topmost = true;
            this.ShowInTaskbar = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();

            }
            catch (System.Exception)
            { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
