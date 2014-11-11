using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DotGame
{
    class BasicGraphic : IGraphic
    {
        protected Canvas Canvas;
        private List<Tuple<Dot, Ellipse>> ListDot;
        protected List<Tuple<Microsoft.Xna.Framework.Rectangle, Rectangle>> HitBoxes;

        protected Timer Timer;
        private Rectangle BackgroundTimer;

        public event DotEventHandler TapDot;

        public BasicGraphic(Canvas canvas)
        {
            ListDot = new List<Tuple<Dot,Ellipse>>();
            HitBoxes = new List<Tuple<Microsoft.Xna.Framework.Rectangle,Rectangle>>();
            Canvas = canvas;
        }

        public virtual void DrawDot(Dot dot)
        {
            var ellipse = new Ellipse();

            ListDot.Add(new Tuple<Dot, Ellipse>(dot, ellipse));
            switch (dot.Type)
            {
                case DotType.Coin:
                    ellipse.Fill = new SolidColorBrush(Colors.Blue);
                    break;
                case DotType.Malus:
                    ellipse.Fill = new SolidColorBrush(Colors.Red);
                    break;
                default:
                    ellipse.Fill = new SolidColorBrush(Colors.Blue);
                    break;
            }
            ellipse.Height = dot.size;
            ellipse.Width = dot.size;
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Top;
            ellipse.Tap += ellipse_Tap;

            Canvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, dot.TopLeft.X);
            Canvas.SetTop(ellipse, dot.TopLeft.Y);
        }

        void ellipse_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var ellipse = (Ellipse)(sender);
            int index = ListDot.FindIndex(t => t.Item2 == ellipse);
            if (index != -1)
            {
                var dot = ListDot[index].Item1;
                if (TapDot != null)
                    TapDot(dot, new DotEvent("tap"));
            }
        }

        protected virtual void OnTapDot(object sender, DotEvent e)
        {
            DotEventHandler handler = TapDot;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public virtual void UndrawDot(Dot dot)
        {
            int index = ListDot.FindIndex(t => t.Item1 == dot);
            if (index != -1) {
                var ellipse = ListDot[index].Item2;
                Canvas.Children.Remove(ellipse);
                ListDot.RemoveAt(index);
            }
        }

        public void DrawHitBox(Microsoft.Xna.Framework.Rectangle hitBox)
        {
            var rectangle = new Rectangle();

            HitBoxes.Add(new Tuple<Microsoft.Xna.Framework.Rectangle, Rectangle>(hitBox, rectangle));

            rectangle.Stroke = new SolidColorBrush(Colors.Green);
            rectangle.Height = hitBox.Height;
            rectangle.Width = hitBox.Width;
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.VerticalAlignment = VerticalAlignment.Top;

            Canvas.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, hitBox.X);
            Canvas.SetTop(rectangle, hitBox.Y);
        }

        public void UndrawHitBox(Microsoft.Xna.Framework.Rectangle hitBox)
        {
            int index = HitBoxes.FindIndex(t => t.Item1 == hitBox);
            if (index != -1)
            {
                var rectangle = HitBoxes[index].Item2;
                Canvas.Children.Remove(rectangle);
                HitBoxes.RemoveAt(index);
            }
        }

        public virtual void ClearScreen()
        {
            ListDot.Clear();
            HitBoxes.Clear();
            Canvas.Children.Clear();
        }

        public void DrawTimer(Timer timer)
        {
            Timer = timer;
            BackgroundTimer = new Rectangle();
            var color = new SolidColorBrush(Colors.Blue);
            BackgroundTimer.MaxHeight = Canvas.ActualHeight;
            BackgroundTimer.MaxWidth = Canvas.ActualWidth;
            BackgroundTimer.Width = Canvas.ActualWidth;
            BackgroundTimer.Height = Canvas.ActualHeight / 2;
            BackgroundTimer.Fill = color;
            BackgroundTimer.HorizontalAlignment = HorizontalAlignment.Left;
            BackgroundTimer.VerticalAlignment = VerticalAlignment.Top;
            BackgroundTimer.RenderTransform = new TranslateTransform() { Y = Canvas.ActualHeight /2 };
            Canvas.Children.Add(BackgroundTimer);
            Timer.PropertyChanged += Timer_PropertyChanged;
        }

        void Timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var size = System.Math.Abs(Timer.Time.TotalSeconds * (Canvas.ActualHeight / (Timer.InitTime.TotalSeconds * 2)));
            BackgroundTimer.Height = (size);
            if (BackgroundTimer.Height < BackgroundTimer.MaxHeight)
                BackgroundTimer.RenderTransform = new TranslateTransform() { Y = Canvas.ActualHeight - size };
        }
    }
}
