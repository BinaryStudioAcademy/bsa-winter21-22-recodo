using Emgu.CV;
using Emgu.CV.Structure;
using Recodo.Desktop.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    Mat m = new Mat();  // обьект для хранения картинки
                    _cameraService.Retrieve(m);
                    if (_cameraService.Retrieve(m))
                    {
                        imageBox.Source = Convert(m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap());
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
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
                _cameraService.selectedCameraId = deviceId-1;
                _cameraService.ImageGrabbed += Capture_ImageGrabbed;
                _cameraService.Notify += Close;
                _cameraService.StartCapture(deviceId - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

    }
}
