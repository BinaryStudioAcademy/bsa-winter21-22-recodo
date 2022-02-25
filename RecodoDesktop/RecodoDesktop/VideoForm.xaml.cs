using Emgu.CV;
using Recodo.Desktop.Logic;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Recodo.Desktop.Main
{
    /// <summary>
    /// Interaction logic for VideoForm.xaml
    /// </summary>
    public partial class VideoForm : Window
    {
        CameraService _cameraService = CameraService.GetInstance();
        private int deviceId;

        public VideoForm(int deviceId)
        {
            InitializeComponent();
            this.deviceId = deviceId;
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            var v = _cameraService.QueryFrame();
            if (v != null)
            {
                Dispatcher.Invoke(() =>
                {
                    imageBox.ImageSource = Convert(v.ToBitmap());
                });
            }
        }

        private BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _cameraService.ImageGrabbed += Capture_ImageGrabbed;
                _cameraService.Notify += Close;
                _cameraService.StartCapture(deviceId - 1);

                this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) * 0.9;
                this.Left = 20;
                this.Topmost = true;
                this.ShowInTaskbar = true;

                //imageBox.Source = new BitmapImage(new Uri(@"\Icons\camera_preview.png"));
                LinearGradientBrush gradientBrush = new LinearGradientBrush(
                    System.Windows.Media.Color.FromRgb(255, 0, 0),
                    System.Windows.Media.Color.FromRgb(246, 149, 120),
                    new System.Windows.Point(0.75, 0), new System.Windows.Point(0.25, 1));
                mBorder.Background = gradientBrush;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
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
