using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Animation;


namespace DotGame
{
    public enum DotType { Coin, Malus, Bonus };
    class Dot
    {
        public Point TopLeft;
        public Microsoft.Xna.Framework.Rectangle SquareCollision;

        public int size;
        private TimeSpan time;
        public TimeSpan Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                if (time <= TimeSpan.Zero && dotExplode != null)
                {
                    dotExplode(this, new DotEvent("boum"));
                }
            } 
        }
        public TimeSpan initTime;
        public DotType Type;

        public delegate void DotEventHandler(object source, DotEvent e);
        public event DotEventHandler dotExplode;

        public Dot(int x, int y,  int size = 100, DotType type = DotType.Coin, TimeSpan? time = null)
        {
            TopLeft = new Point(x, y);
            SquareCollision = new Microsoft.Xna.Framework.Rectangle(x, y, size, size);
            Type = type;
            this.size = size;
            if (time == null)
            {
                time = new TimeSpan(0, 0, 0, 0, 600);
            }
            this.initTime = (TimeSpan)(time);
            this.time = this.initTime;
        }
    }
}
