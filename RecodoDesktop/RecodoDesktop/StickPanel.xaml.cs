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
        private TimeSpan time = TimeSpan.FromSeconds(0);
        private DispatcherTimer Timer;

        public StickPanel(RecorderService recorderService)
        {
            _recorderService = recorderService;

            InitializeComponent();

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            TimeLabel.Content = TimeSpan.FromSeconds(0).ToString(@"m\:ss");

            this.Top = 70;
            this.Left = 0;
            this.Topmost = true;
            this.ShowInTaskbar = true;

            _recorderService.StartRec += _recorderService_StartRec;
        }

        private void _recorderService_StartRec()
        {
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
                _recorderService.StopRecording();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (System.Exception)
            { }
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            _recorderService.StopRecording();
            Timer.Stop();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
