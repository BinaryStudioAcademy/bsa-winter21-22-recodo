using System;
using System.Windows;
using System.Windows.Threading;

namespace Recodo.Desktop.Main
{
	public partial class Countdown : Window
	{
        DispatcherTimer _timer;
        TimeSpan _time;
		public Countdown()
		{
			InitializeComponent();
            _time = TimeSpan.FromSeconds(3);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    countdown.Content = _time.Seconds.ToString();
                    if (_time == TimeSpan.Zero) 
                    {
                        _timer.Stop();
                        Hide();
                    }
                    _time = _time.Add(TimeSpan.FromSeconds(-1));                    
                }, Application.Current.Dispatcher);
			_timer.Start();
		}
	}
}