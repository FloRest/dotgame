using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DotGame
{
    class Timer : INotifyPropertyChanged
    {
        private DispatcherTimer timer;
        private DateTime timeElapsed;
        private DateTime timeAtStart;

        public delegate void DotEventHandler(object source, DotEvent e);
        public event DotEventHandler TimerEnd;
        public TimeSpan Time {get; set; }
        public TimeSpan InitTime { get; set; }

        public const float timeBeetweenTick = 50;

        public Timer(TimeSpan initTime)
        {
            this.Time = initTime;
            this.InitTime = initTime;
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(timeBeetweenTick);
            this.timer.Tick += timer_Tick;
        }

        public void Start()
        {
            this.timeElapsed = DateTime.Now;
            this.timeAtStart = DateTime.Now;
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        public TimeSpan GetTimeFromStart()
        {
            return timeAtStart - DateTime.Now;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (this.Time <= TimeSpan.Zero)
            {
                if (TimerEnd != null)
                    TimerEnd(this, new DotEvent("TimeEnd"));
            }
            this.Time -= DateTime.Now - this.timeElapsed;
            this.timeElapsed = DateTime.Now;
            NotifyPropertyChanged("Time");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}
