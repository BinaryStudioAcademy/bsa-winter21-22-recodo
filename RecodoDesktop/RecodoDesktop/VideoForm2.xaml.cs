using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Recodo.Desktop.Main
{
    /// <summary>
    /// Interaction logic for VideoForm2.xaml
    /// </summary>
    public partial class VideoForm2 : Window
    {
        public VideoForm2()
        {
            InitializeComponent();

            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) * 0.9;
            this.Left = 20;
            this.Topmost = true;
            this.ShowInTaskbar = true;

            //imageBox.Source = new BitmapImage(new Uri(@"\Icons\camera_preview.png"));
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                Color.FromRgb(255, 0, 0),
                Color.FromRgb(246, 149, 120),
                new Point(0.75, 0), new Point(0.25, 1));
            mBorder.Background = gradientBrush;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception)
            { }
        }
    }
}
