using System.Windows;
using System.Windows.Input;

namespace RecodoDesktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;
            this.Topmost = true;
            this.ShowInTaskbar = true;
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
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
    }
}
