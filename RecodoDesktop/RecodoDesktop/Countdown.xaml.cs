using Recodo.Desktop.Logic;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Recodo.Desktop.Main
{
    public partial class Countdown : Window
    {
        private RecorderService _recorderService;
        private CameraService _cameraService;
        DispatcherTimer Timer;
        TimeSpan time = TimeSpan.FromSeconds(3);

        public Countdown(RecorderService recorderService, CameraService cameraService)
        {
            InitializeComponent();
            _recorderService = recorderService;
            _cameraService = cameraService;
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
            countdown.Content = time.Seconds.ToString();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time = time.Add(TimeSpan.FromSeconds(-1));

            if (time != TimeSpan.Zero)
            {
                countdown.Content = time.Seconds.ToString();
            }
            else
            {
                Timer.Stop();
                StickPanel stickPanel = new StickPanel(_recorderService, _cameraService);
                stickPanel.Show();
                this.Hide();

                _recorderService.StartRecording();
            }
        }
    }
}