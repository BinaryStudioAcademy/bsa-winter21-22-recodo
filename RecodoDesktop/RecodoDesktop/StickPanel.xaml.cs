using Recodo.Desktop.Logic;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Recodo.Desktop.Main
{
    /// <summary>
    /// Interaction logic for StickPanel.xaml
    /// </summary>
    public partial class StickPanel : Window
    {
        private readonly RecorderService _recorderService;
        private readonly CameraService _cameraService;
        private TimeSpan time = TimeSpan.FromSeconds(0);
        private DispatcherTimer Timer;
        private bool isPause = false;

        public StickPanel(RecorderService recorderService, CameraService cameraService)
        {
            _recorderService = recorderService;
            _cameraService = cameraService; 

            InitializeComponent();

            TimeLabel.Content = TimeSpan.FromSeconds(0).ToString(@"m\:ss");

            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height)/ 2;
            this.Left = 0;
            this.Topmost = true;
            this.ShowInTaskbar = true;

            _recorderService.StartRec += _recorderService_StartRec;
        }

        private void _recorderService_StartRec()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            if (time != TimeSpan.FromSeconds(300))
            {
                TimeLabel.Content = time.ToString(@"m\:ss");
            }
            else
            {
                Timer.Stop();
                _cameraService.StopCapture();
                _recorderService.StopRecording();
                this.Close();
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

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            _cameraService.StopCapture();
            _recorderService.StopRecording();
            if (Timer is not null)
            {
                Timer.Stop();
                this.Close();
            }
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            isPause = !isPause;

            if (isPause)
            {
                _recorderService.PauseRecording();
                Timer?.Stop();
            }
            else
            {
                _recorderService.ResumeRecording();
                Timer?.Start();
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            Timer?.Stop();
            time = TimeSpan.FromSeconds(0);
            TimeLabel.Content = TimeSpan.FromSeconds(0).ToString(@"m\:ss");
            _recorderService.RestartRecording();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            _recorderService.CancelRecording();
            if (Timer is not null)
            {
                Timer.Stop();
                this.Close();
            }
        }
    }
}
