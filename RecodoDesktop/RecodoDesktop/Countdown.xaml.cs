using Recodo.Desktop.Logic;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Recodo.Desktop.Main
{
	public partial class Countdown : Window
	{
        private RecorderService _recorderService;
        DispatcherTimer Timer;
        TimeSpan time = TimeSpan.FromSeconds(3);

		public Countdown(RecorderService recorderService)
		{
			InitializeComponent();
            _recorderService = recorderService;
            Timer = new DispatcherTimer();
            Timer.Interval = new System.TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
            countdown.Content = time.Seconds.ToString();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            time = time.Add(TimeSpan.FromSeconds(-1));

            if (time != TimeSpan.Zero)
            {
                countdown.Content = time.Seconds.ToString();
            }
            else
            {
                Timer.Stop();
                this.Hide();
                _recorderService.StartRecording();
            }
        }
    }
}