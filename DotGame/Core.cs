using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DotGame
{
    class Core
    {
        public Timer Timer;
        public Score Score;
        public ObservableCollection<Dot> Dots;
        private int maxDots;

        private Rectangle GameArea;

        private int Seed;

        private IGraphic Graphic;
        private ISound Sound;
        private IHud Hud;

        public Core(IGraphic graphic, ISound sound, IHud hud)
        {
            Score = new Score(0, 1);
            Timer = new Timer(new TimeSpan(0,0,10));
            Timer.PropertyChanged += Timer_PropertyChanged;
            Timer.TimerEnd += Timer_TimerEnd;
            Dots = new ObservableCollection<Dot>();
            Dots.CollectionChanged += Dots_CollectionChanged;
            maxDots = 3;
            GameArea.X = 0;
            GameArea.Y = 0;
            GameArea.Width = (int)(Application.Current.Host.Content.ActualWidth);
            GameArea.Height = (int)(Application.Current.Host.Content.ActualHeight) - 100;
            Seed = (new Random()).Next();
            Graphic = graphic;
            Sound = sound;
            Hud = hud;
        }

        void Timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            foreach (var dot in Dots.ToList())
            {
                dot.Time -= new TimeSpan(0, 0, 0, 0, 50);
            }
        }

        void Timer_TimerEnd(object source, DotEvent e)
        {
            Timer.Stop();
            Graphic.ClearScreen();
        }


        void Dots_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                var rnd = new Random();
                while (Dots.Count < maxDots)
                {
                    int i = rnd.Next(0, 9);
                    DotType type = i == 8 ? DotType.Malus : DotType.Coin;
                    if (type == DotType.Malus)
                    {
                        type = DotType.Coin;
                        foreach (var dot in Dots.ToList())
                        {
                            if (dot.Type == DotType.Coin)
                            {
                                type = DotType.Malus;
                                break;
                            }
                        }
                    }
                    var d = CreateDot(100, type);
                    Dots.Add(d);
                    d.dotExplode += d_dotExplode;
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var dot = Dots[Dots.Count - 1];
                Graphic.DrawDot(dot);
                //Graphic.DrawHitBox(dot.SquareCollision); 
            }
        }

        void d_dotExplode(object source, DotEvent e)
        {
            var dot = (Dot)(source);
            if (dot.Type == DotType.Coin)
            {
                Timer.Time -= dot.initTime;
            }
            Graphic.UndrawDot(dot);
            Graphic.UndrawHitBox(dot.SquareCollision);
            Dots.Remove(dot);
        }

        public void Play()
        {
            Graphic.TapDot += Graphic_TapDot;
            Graphic.DrawTimer(Timer);
            Hud.DrawScore(Score);
            for (int i = 0; i < maxDots; i++)
            {
                Dots.Add(CreateDot(100));
            }
        }

        public void Stop()
        {
            Timer.Stop();
            Graphic.ClearScreen();
            Dots.Clear();
            Score.Reset();
        }

        void Graphic_TapDot(object source, DotEvent e)
        {
            Timer.Start();
            var dot = (Dot)(source);
            int math = (int)(Math.Sqrt(Score.Points / 10));

            switch (dot.Type)
            {
                case DotType.Coin:
                    Sound.PlayCoinSound();
                    Score.Add();
                    Timer.Time += dot.Time;
                    break;
                case DotType.Malus:
                    Sound.PlayWrongSound();
                    Timer.Time -= dot.initTime;
                    break;
            }
            
            //calc max dots (square root)
            maxDots = math > 3 ? math : 3;
            
            Graphic.UndrawDot(dot);
            Graphic.UndrawHitBox(dot.SquareCollision); 
            Dots.Remove(dot);
        }

        public Dot CreateDot(int width, DotType type = DotType.Coin)
        {
            Random rnd;
            bool collision = false;
            System.Windows.Point TopLeft = new System.Windows.Point(0, 0);
            int i = 0;

            do
            {
                rnd = new Random(Seed);
                Seed += Seed / 2;
                TopLeft.X = rnd.Next(GameArea.X, GameArea.Width - width);
                TopLeft.Y = rnd.Next(GameArea.Y, GameArea.Height - width);
                foreach (var dot in Dots)
                {
                    collision = Collision(new Rectangle((int)(TopLeft.X), (int)(TopLeft.Y), width, width), dot.SquareCollision);
                    if (collision == true)
                        break;
                }
                i++;
            } while (collision == true && i <= 100);

            return new Dot((int)(TopLeft.X), (int)(TopLeft.Y), width, type);
        }

        public bool Collision(Rectangle one, Rectangle two)
        {
            return one.Intersects(two);
        }
    }
}
