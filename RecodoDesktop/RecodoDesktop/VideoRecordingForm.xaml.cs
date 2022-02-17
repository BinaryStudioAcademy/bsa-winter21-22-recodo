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
//using ScreenRecorderLib;

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
    }
}
