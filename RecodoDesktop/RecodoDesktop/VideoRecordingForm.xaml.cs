using Recodo.Desktop.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for VideoRecordingForm.xaml
    /// </summary>
    public partial class VideoRecordingForm : Window
    {
        private bool videoFormOpened = false;

        public delegate void SelectionChanged();

        public event SelectionChanged Notify;

        CameraService _cameraService = CameraService.GetInstance();
        public VideoRecordingForm()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var devices = _cameraService.GetCameras().ToList();
            for (int i = 0; i < devices.Count; i++)
            {
                cameraComboBox.Items.Add(devices[i]);
            }      
        }

        private void cameraComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoForm videoForm = new VideoForm(cameraComboBox.SelectedIndex);
            
            if (!videoFormOpened)
            {
                videoFormOpened = true;
                return;
            }
            if (cameraComboBox.SelectedIndex != 0)
            {
                videoForm.Show();
            }
            else
            {
                
                _cameraService.StopCapture();
               
            }
            
            
        }

    }
}
