using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotGame
{
    class Score : INotifyPropertyChanged
    {
        public uint Points { get; set; }
        public uint Point { get; set; }

        public Score(uint score = 0, uint point = 1)
        {
            Points = score;
            Point = point;
        }

        public uint Add(uint point = 0)
        {
            if (point == 0)
            {
                Points += Point;
            }
            else
            {
                Points += point;
            }
            NotifyPropertyChanged("Points");
            return Points;
        }

        public uint Sub(uint point = 0)
        {
            if (point == 0)
            {
                Points -= Point;
            }
            else
            {
                Points -= point;
            }
            NotifyPropertyChanged("Points");
            return Points;
        }

        public void Reset()
        {
            Points = 0;
            NotifyPropertyChanged("Points");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}
