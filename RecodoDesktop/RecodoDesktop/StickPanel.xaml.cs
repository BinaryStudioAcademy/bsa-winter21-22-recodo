using System.Windows;
using System.Windows.Input;

namespace Recodo.Desktop.Main
{
    /// <summary>
    /// Interaction logic for StickPanel.xaml
    /// </summary>
    public partial class StickPanel : Window
    {
        public StickPanel()
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
