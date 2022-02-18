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
        private readonly RecorderService _recorderService;
        private RecorderConfiguration _options;

        private List<string> audioDevices;
        private List<string> recordableWindows;

        private bool inputAudiButtonIsActive = true;
        private bool videoFormOpened = false;
        CameraService _cameraService = CameraService.GetInstance();
        
        public VideoRecordingForm(RecorderService recorderService)
        {
            _recorderService = recorderService;
            _options = new RecorderConfiguration();

            InitializeComponent();
            _recorderService.Configure(_options);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Countdown countdown = new(_recorderService);
            countdown.Topmost = true;
            this.Hide();
            countdown.Show();
        }

        private void AudioDevices_Initialized(object sender, EventArgs e)
        {
            audioDevices = _recorderService.GetInputAudioDevices();

            this.AudioDevices.ItemsSource = audioDevices;
            this.AudioDevices.SelectedIndex = 0;
        }

        private void Microphone_Click(object sender, RoutedEventArgs e)
        {
            this.Microphone.Content = FindResource(this.Microphone.Content == FindResource("Mic") ? "MicOff" : "Mic");
            inputAudiButtonIsActive = !inputAudiButtonIsActive;
            _options.IsAudioEnable = inputAudiButtonIsActive;
        }

        private void AudioDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _options.SelectedAudioInputDevice = this.AudioDevices.SelectedItem.ToString();
        }

        private void RecordableWindows_Initialized(object sender, EventArgs e)
        {
            recordableWindows = _recorderService.GetWindows();
            recordableWindows.Add("Full screen");
            this.RecordableWindows.ItemsSource = recordableWindows;
            this.RecordableWindows.SelectedIndex = recordableWindows.Count-1;
        }

        private void RecordableWindows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _options.RecorderWindowTitle = this.RecordableWindows.SelectedItem.ToString(); 
        }

        private void RecordingResolution_Initialized(object sender, EventArgs e)
        {
            var resolutions = RecorderHelper.GetNamesOfResolutions();
            this.RecordingResolution.ItemsSource = resolutions;
            this.RecordingResolution.SelectedIndex = resolutions.Count - 1;
        }

        private void RecordingResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedResolutionName = RecordingResolution.SelectedItem.ToString();
            var selectedResolution = RecorderHelper.GetResolutionByName(selectedResolutionName);
            _options.Resolution = selectedResolution;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> devices = _cameraService.GetCameras().ToList();
            for (int i = 0; i < devices.Count; i++)
            {
                cameraComboBox.Items.Add(devices[i]);
            }
        }

        private void cameraComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                VideoForm videoForm = new VideoForm(cameraComboBox.SelectedIndex);
            
                if (!videoFormOpened)
                {
                        videoFormOpened = true;
                        return;
                }
                if (cameraComboBox.SelectedIndex != 0)
                {
                        _cameraService.StopCapture();
                        videoForm.Show();
                }
                else
                {
                        _cameraService.StopCapture();               
                }
            });
        }
    }
}
